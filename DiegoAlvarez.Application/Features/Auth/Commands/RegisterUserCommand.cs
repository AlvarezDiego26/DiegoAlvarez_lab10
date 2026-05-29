using DiegoAlvarez.Application.DTOs.Auth;
using DiegoAlvarez.Application.Exceptions;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Domain.Entities;
using MediatR;

namespace DiegoAlvarez.Application.Features.Auth.Commands;

public record RegisterUserCommand(RegisterRequestDto Dto) : IRequest<RegisterResponseDto>;

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponseDto>
{
    private readonly IUnitOfWork _uow;

    public RegisterUserCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<RegisterResponseDto> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _uow.Users.GetByUsernameAsync(request.Dto.Username);

        if (exists is not null)
            throw new ConflictException("El username ya existe.");

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
            Username = request.Dto.Username,
            Email = request.Dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password),
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
            "Usuario registrado correctamente.");
    }
}
