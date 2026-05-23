using DiegoAlvarez.Application.Interfaces.Repositories;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Infrastructure.Context;
using DiegoAlvarez.Infrastructure.Implementations.Repositories;

namespace DiegoAlvarez.Infrastructure.Implementations.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _ctx;
    public IUserRepository Users { get; }
    public ITicketRepository Tickets { get; }
    public IResponseRepository Responses { get; }
    public IRoleRepository Roles { get; }

    public UnitOfWork(AppDbContext ctx)
    {
        _ctx = ctx;
        Users = new UserRepository(ctx);
        Tickets = new TicketRepository(ctx);
        Responses = new ResponseRepository(ctx);
        Roles = new RoleRepository(ctx);
    }

    public async Task<int> SaveChangesAsync() => await _ctx.SaveChangesAsync();
    public void Dispose() => _ctx.Dispose();
}
