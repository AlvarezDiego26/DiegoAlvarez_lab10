using DiegoAlvarez.Application.DTOs.Response;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using MediatR;

namespace DiegoAlvarez.Application.Features.Responses.Queries;

public record GetResponsesByTicketQuery(Guid TicketId) : IRequest<IEnumerable<ResponseDto>>;

internal class GetResponsesByTicketQueryHandler
    : IRequestHandler<GetResponsesByTicketQuery, IEnumerable<ResponseDto>>
{
    private readonly IUnitOfWork _uow;

    public GetResponsesByTicketQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<ResponseDto>> Handle(
        GetResponsesByTicketQuery request,
        CancellationToken cancellationToken)
    {
        var responses = await _uow.Responses.GetByTicketIdAsync(request.TicketId);

        return responses.Select(r => new ResponseDto(
            r.ResponseId,
            r.Message,
            r.CreatedAt,
            r.Responder.Username));
    }
}
