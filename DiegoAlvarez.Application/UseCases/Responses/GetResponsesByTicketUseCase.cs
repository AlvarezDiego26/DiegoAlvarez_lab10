using DiegoAlvarez.Application.DTOs.Response;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;

namespace DiegoAlvarez.Application.UseCases.Responses;

public class GetResponsesByTicketUseCase
{
    private readonly IUnitOfWork _uow;

    public GetResponsesByTicketUseCase(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<ResponseDto>> ExecuteAsync(Guid ticketId)
    {
        var responses = await _uow.Responses.GetByTicketIdAsync(ticketId);

        return responses.Select(r => new ResponseDto(
            r.ResponseId,
            r.Message,
            r.CreatedAt,
            r.Responder.Username));
    }
}
