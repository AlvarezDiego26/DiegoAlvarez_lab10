using DiegoAlvarez.Domain.Entities;

namespace DiegoAlvarez.Application.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name);
    Task AddAsync(Role role);
}
