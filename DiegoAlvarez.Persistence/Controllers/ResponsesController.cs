using System.Security.Claims;
using DiegoAlvarez.Application.DTOs.Response;
using DiegoAlvarez.Application.UseCases.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiegoAlvarez.Persistence.Controllers;

[ApiController]
[Route("api/tickets/{ticketId}/responses")]
[Authorize]
public class ResponsesController : ControllerBase
{
    private readonly GetResponsesByTicketUseCase _getResponses;
    private readonly CreateResponseUseCase _createResponse;

    public ResponsesController(
        GetResponsesByTicketUseCase getResponses,
        CreateResponseUseCase createResponse)
    {
        _getResponses = getResponses;
        _createResponse = createResponse;
    }

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetByTicket(Guid ticketId)
    {
        var result = await _getResponses.ExecuteAsync(ticketId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        Guid ticketId,
        CreateResponseDto dto)
    {
        try
        {
            var responseId = await _createResponse.ExecuteAsync(
                ticketId,
                CurrentUserId,
                dto);

            return Ok(new { responseId });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
