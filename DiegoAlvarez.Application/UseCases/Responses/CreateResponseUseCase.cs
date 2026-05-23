using DiegoAlvarez.Application.DTOs.Response;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Domain.Entities;

namespace DiegoAlvarez.Application.UseCases.Responses;

public class CreateResponseUseCase
{
    private readonly IUnitOfWork _uow;

    public CreateResponseUseCase(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> ExecuteAsync(
        Guid ticketId,
        Guid responderId,
        CreateResponseDto dto)
    {
        var ticket = await _uow.Tickets.GetByIdAsync(ticketId);

        if (ticket is null)
            throw new Exception("Ticket no encontrado.");

        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = ticketId,
            ResponderId = responderId,
            Message = dto.Message,
            CreatedAt = DateTime.Now
        };

        await _uow.Responses.AddAsync(response);

        await _uow.SaveChangesAsync();

        return response.ResponseId;
    }
}
