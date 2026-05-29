using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Features.Tickets.Commands;
using DiegoAlvarez.Application.Features.Tickets.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiegoAlvarez.Persistence.Controllers;

[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllTicketsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetTicketByIdQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketDto dto)
    {
        return Ok(await _mediator.Send(new CreateTicketCommand(dto)));
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateTicketStatusDto dto)
    {
        return Ok(await _mediator.Send(new UpdateTicketStatusCommand(id, dto)));
    }
}
