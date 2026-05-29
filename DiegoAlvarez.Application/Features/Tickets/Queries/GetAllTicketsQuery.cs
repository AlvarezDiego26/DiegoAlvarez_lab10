using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using MediatR;

namespace DiegoAlvarez.Application.Features.Tickets.Queries;

public record GetAllTicketsQuery : IRequest<IEnumerable<TicketDto>>;

internal class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllTicketsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<TicketDto>> Handle(
        GetAllTicketsQuery request,
        CancellationToken cancellationToken)
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
