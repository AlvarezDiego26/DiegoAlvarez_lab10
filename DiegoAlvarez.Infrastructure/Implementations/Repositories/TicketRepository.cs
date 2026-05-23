using DiegoAlvarez.Application.Interfaces.Repositories;
using DiegoAlvarez.Domain.Entities;
using DiegoAlvarez.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DiegoAlvarez.Infrastructure.Implementations.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _ctx;
    public TicketRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Ticket?> GetByIdAsync(Guid id) =>
        await _ctx.Tickets
            .Include(t => t.User)
            .Include(t => t.Responses).ThenInclude(r => r.Responder)
            .FirstOrDefaultAsync(t => t.TicketId == id);

    public async Task<IEnumerable<Ticket>> GetAllAsync() =>
        await _ctx.Tickets.Include(t => t.User).ToListAsync();

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId) =>
        await _ctx.Tickets.Where(t => t.UserId == userId).ToListAsync();

    public async Task AddAsync(Ticket ticket) => await _ctx.Tickets.AddAsync(ticket);
    public void Update(Ticket ticket) => _ctx.Tickets.Update(ticket);
    public void Delete(Ticket ticket) => _ctx.Tickets.Remove(ticket);
}
