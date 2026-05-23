using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;

namespace DiegoAlvarez.Application.UseCases.Tickets;

public class GetTicketByIdUseCase
{
    private readonly IUnitOfWork _uow;

    public GetTicketByIdUseCase(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<TicketDto?> ExecuteAsync(Guid id)
    {
        var t = await _uow.Tickets.GetByIdAsync(id);

        if (t is null)
            return null;

        return new TicketDto(
            t.TicketId,
            t.Title,
            t.Description,
            t.Status,
            t.CreatedAt,
            t.User.Username);
    }
}
