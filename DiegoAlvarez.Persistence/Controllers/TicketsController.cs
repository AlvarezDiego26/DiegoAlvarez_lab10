using System.Security.Claims;
using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.UseCases.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiegoAlvarez.Persistence.Controllers;

[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly GetAllTicketsUseCase _getAllTickets;
    private readonly GetTicketByIdUseCase _getTicketById;
    private readonly CreateTicketUseCase _createTicket;
    private readonly UpdateTicketStatusUseCase _updateStatus;

    public TicketsController(
        GetAllTicketsUseCase getAllTickets,
        GetTicketByIdUseCase getTicketById,
        CreateTicketUseCase createTicket,
        UpdateTicketStatusUseCase updateStatus)
    {
        _getAllTickets = getAllTickets;
        _getTicketById = getTicketById;
        _createTicket = createTicket;
        _updateStatus = updateStatus;
    }

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllTickets.ExecuteAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getTicketById.ExecuteAsync(id);

        if (result is null)
            return NotFound(new { message = "Ticket no encontrado." });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketDto dto)
    {
        var ticketId = await _createTicket.ExecuteAsync(CurrentUserId, dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = ticketId },
            new { ticketId });
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateTicketStatusDto dto)
    {
        try
        {
            await _updateStatus.ExecuteAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
