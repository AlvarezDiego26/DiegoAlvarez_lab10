using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Domain.Entities;

namespace DiegoAlvarez.Application.UseCases.Tickets;

public class CreateTicketUseCase
{
    private readonly IUnitOfWork _uow;

    public CreateTicketUseCase(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> ExecuteAsync(Guid userId, CreateTicketDto dto)
    {
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = userId,
            Title = dto.Title,
            Description = dto.Description,
            Status = "abierto",
            CreatedAt = DateTime.Now
        };

        await _uow.Tickets.AddAsync(ticket);
        await _uow.SaveChangesAsync();

        return ticket.TicketId;
    }
}
