using DiegoAlvarez.Application.DTOs.Response;
using DiegoAlvarez.Application.Exceptions;
using DiegoAlvarez.Application.Interfaces.Security;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Domain.Entities;
using MediatR;

namespace DiegoAlvarez.Application.Features.Responses.Commands;

public record CreateResponseCommand(
    Guid TicketId,
    CreateResponseDto Dto) : IRequest<Guid>;

internal class CreateResponseCommandHandler : IRequestHandler<CreateResponseCommand, Guid>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _uow;

    public CreateResponseCommandHandler(ICurrentUserService currentUser, IUnitOfWork uow)
    {
        _currentUser = currentUser;
        _uow = uow;
    }

    public async Task<Guid> Handle(
        CreateResponseCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await _uow.Tickets.GetByIdAsync(request.TicketId);

        if (ticket is null)
            throw new NotFoundException("Ticket no encontrado.");

        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = request.TicketId,
            ResponderId = _currentUser.UserId,
            Message = request.Dto.Message,
            CreatedAt = DateTime.Now
        };

        await _uow.Responses.AddAsync(response);
        await _uow.SaveChangesAsync();

        return response.ResponseId;
    }
}
