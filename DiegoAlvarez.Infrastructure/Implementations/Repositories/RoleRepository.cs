using DiegoAlvarez.Application.Interfaces.Repositories;
using DiegoAlvarez.Domain.Entities;
using DiegoAlvarez.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DiegoAlvarez.Infrastructure.Implementations.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _ctx;
    public RoleRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Role?> GetByNameAsync(string name) =>
        await _ctx.Roles.FirstOrDefaultAsync(r => r.RoleName == name);

    public async Task AddAsync(Role role) => await _ctx.Roles.AddAsync(role);
}
