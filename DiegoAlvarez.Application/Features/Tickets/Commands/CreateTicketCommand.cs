using DiegoAlvarez.Application.DTOs.Ticket;
using DiegoAlvarez.Application.Interfaces.Security;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Domain.Entities;
using MediatR;

namespace DiegoAlvarez.Application.Features.Tickets.Commands;

public record CreateTicketCommand(CreateTicketDto Dto) : IRequest<Guid>;

internal class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Guid>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _uow;

    public CreateTicketCommandHandler(ICurrentUserService currentUser, IUnitOfWork uow)
    {
        _currentUser = currentUser;
        _uow = uow;
    }

    public async Task<Guid> Handle(
        CreateTicketCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = _currentUser.UserId,
            Title = request.Dto.Title,
            Description = request.Dto.Description,
            Status = "abierto",
            CreatedAt = DateTime.Now
        };

        await _uow.Tickets.AddAsync(ticket);
        await _uow.SaveChangesAsync();

        return ticket.TicketId;
    }
}
