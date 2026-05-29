using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Exceptions;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using MediatR;

namespace DiegoAlvarez.Application.Features.Tickets.Commands;

public record UpdateTicketStatusCommand(Guid TicketId, UpdateTicketStatusDto Dto) : IRequest<bool>;

internal class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketStatusCommand, bool>
{
    private readonly IUnitOfWork _uow;

    public UpdateTicketStatusCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<bool> Handle(
        UpdateTicketStatusCommand request,
        CancellationToken cancellationToken)
    {
        var valid = new[] { "abierto", "en_proceso", "cerrado" };

        if (!valid.Contains(request.Dto.Status))
            throw new ValidationException("Estado invalido. Use: abierto, en_proceso, cerrado.");

        var ticket = await _uow.Tickets.GetByIdAsync(request.TicketId);

        if (ticket is null)
            throw new NotFoundException("Ticket no encontrado.");

        ticket.Status = request.Dto.Status;

        if (request.Dto.Status == "cerrado")
            ticket.ClosedAt = DateTime.Now;

        _uow.Tickets.Update(ticket);
        await _uow.SaveChangesAsync();

        return true;
    }
}
