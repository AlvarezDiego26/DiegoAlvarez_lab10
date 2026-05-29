using DiegoAlvarez.Application.DTOs.Response;
using DiegoAlvarez.Application.Features.Responses.Commands;
using DiegoAlvarez.Application.Features.Responses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiegoAlvarez.Persistence.Controllers;

[ApiController]
[Route("api/tickets/{ticketId}/responses")]
[Authorize]
public class ResponsesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetByTicket(Guid ticketId)
    {
        return Ok(await _mediator.Send(new GetResponsesByTicketQuery(ticketId)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        Guid ticketId,
        CreateResponseDto dto)
    {
        return Ok(await _mediator.Send(new CreateResponseCommand(ticketId, dto)));
    }
}
