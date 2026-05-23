using DiegoAlvarez.Application.Interfaces.Repositories;
using DiegoAlvarez.Domain.Entities;
using DiegoAlvarez.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DiegoAlvarez.Infrastructure.Implementations.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;
    public UserRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _ctx.Users
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);

    public async Task<User?> GetByUsernameAsync(string username) =>
        await _ctx.Users
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _ctx.Users
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .ToListAsync();

    public async Task AddAsync(User user) => await _ctx.Users.AddAsync(user);
    public void Update(User user) => _ctx.Users.Update(user);
    public void Delete(User user) => _ctx.Users.Remove(user);
}
