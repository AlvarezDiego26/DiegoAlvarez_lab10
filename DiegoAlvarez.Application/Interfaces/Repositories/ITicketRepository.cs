using DiegoAlvarez.Domain.Entities;

namespace DiegoAlvarez.Application.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(Guid id);
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Ticket ticket);
    void Update(Ticket ticket);
    void Delete(Ticket ticket);
}
