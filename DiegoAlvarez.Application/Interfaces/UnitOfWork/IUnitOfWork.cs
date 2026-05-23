using DiegoAlvarez.Application.Interfaces.Repositories;

namespace DiegoAlvarez.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ITicketRepository Tickets { get; }
    IResponseRepository Responses { get; }
    IRoleRepository Roles { get; }
    Task<int> SaveChangesAsync();
}
