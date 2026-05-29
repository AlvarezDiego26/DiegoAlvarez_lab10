using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Exceptions;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using MediatR;

namespace DiegoAlvarez.Application.Features.Tickets.Queries;

public record GetTicketByIdQuery(Guid TicketId) : IRequest<TicketDto>;

internal class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
{
    private readonly IUnitOfWork _uow;

    public GetTicketByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<TicketDto> Handle(
        GetTicketByIdQuery request,
        CancellationToken cancellationToken)
    {
        var ticket = await _uow.Tickets.GetByIdAsync(request.TicketId);

        if (ticket is null)
            throw new NotFoundException("Ticket no encontrado.");

        return new TicketDto(
            ticket.TicketId,
            ticket.Title,
            ticket.Description,
            ticket.Status,
            ticket.CreatedAt,
            ticket.User.Username);
    }
}
