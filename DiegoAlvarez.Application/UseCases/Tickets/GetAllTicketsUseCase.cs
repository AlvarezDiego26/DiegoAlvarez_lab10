using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;

namespace DiegoAlvarez.Application.UseCases.Tickets;

public class GetAllTicketsUseCase
{
    private readonly IUnitOfWork _uow;

    public GetAllTicketsUseCase(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<TicketDto>> ExecuteAsync()
    {
        var tickets = await _uow.Tickets.GetAllAsync();

        return tickets.Select(t => new TicketDto(
            t.TicketId,
            t.Title,
            t.Description,
            t.Status,
            t.CreatedAt,
            t.User.Username));
    }
}
