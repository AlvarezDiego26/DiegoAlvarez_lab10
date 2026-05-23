using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;

namespace DiegoAlvarez.Application.UseCases.Tickets;

public class UpdateTicketStatusUseCase
{
    private readonly IUnitOfWork _uow;

    public UpdateTicketStatusUseCase(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task ExecuteAsync(Guid ticketId, UpdateTicketStatusDto dto)
    {
        var valid = new[] { "abierto", "en_proceso", "cerrado" };

        if (!valid.Contains(dto.Status))
            throw new Exception(
                "Estado inválido. Use: abierto, en_proceso, cerrado.");

        var ticket = await _uow.Tickets.GetByIdAsync(ticketId);

        if (ticket is null)
            throw new Exception("Ticket no encontrado.");

        ticket.Status = dto.Status;

        if (dto.Status == "cerrado")
            ticket.ClosedAt = DateTime.Now;

        _uow.Tickets.Update(ticket);

        await _uow.SaveChangesAsync();
    }
}
