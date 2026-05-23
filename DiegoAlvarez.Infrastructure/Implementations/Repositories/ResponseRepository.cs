using DiegoAlvarez.Application.Interfaces.Repositories;
using DiegoAlvarez.Domain.Entities;
using DiegoAlvarez.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DiegoAlvarez.Infrastructure.Implementations.Repositories;

public class ResponseRepository : IResponseRepository
{
    private readonly AppDbContext _ctx;
    public ResponseRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId) =>
        await _ctx.Responses
            .Include(r => r.Responder)
            .Where(r => r.TicketId == ticketId)
            .ToListAsync();

    public async Task AddAsync(Response response) => await _ctx.Responses.AddAsync(response);
}
