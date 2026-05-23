using DiegoAlvarez.Application.DTOs.Auth;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Domain.Entities;

namespace DiegoAlvarez.Application.UseCases.Auth;

public class RegisterUserUseCase
{
    private readonly IUnitOfWork _uow;

    public RegisterUserUseCase(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<RegisterResponseDto> ExecuteAsync(RegisterRequestDto dto)
    {
        var exists = await _uow.Users.GetByUsernameAsync(dto.Username);

        if (exists is not null)
            throw new Exception("El username ya existe.");

        var role = await _uow.Roles.GetByNameAsync("User");

        if (role is null)
        {
            role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "User"
            };

            await _uow.Roles.AddAsync(role);
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            CreatedAt = DateTime.Now
        };

        user.UserRoles.Add(new UserRole
        {
            UserId = user.UserId,
            RoleId = role.RoleId,
            AssignedAt = DateTime.Now
        });

        await _uow.Users.AddAsync(user);

        await _uow.SaveChangesAsync();

        return new RegisterResponseDto(
            user.UserId,
            "Usuario registrado correctamente."
        );
    }
}
