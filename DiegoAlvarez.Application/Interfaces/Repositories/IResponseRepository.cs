using DiegoAlvarez.Domain.Entities;

namespace DiegoAlvarez.Application.Interfaces.Repositories;

public interface IResponseRepository
{
    Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId);
    Task AddAsync(Response response);
}
