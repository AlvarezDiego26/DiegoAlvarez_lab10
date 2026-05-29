using DiegoAlvarez.Application.DTOs.Auth;
using DiegoAlvarez.Application.Exceptions;
using DiegoAlvarez.Application.Interfaces.Security;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using MediatR;

namespace DiegoAlvarez.Application.Features.Auth.Queries;

public record LoginQuery(LoginRequestDto Dto) : IRequest<LoginResponseDto>;

internal class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
{
    private readonly IUnitOfWork _uow;
    private readonly ITokenService _token;

    public LoginQueryHandler(IUnitOfWork uow, ITokenService token)
    {
        _uow = uow;
        _token = token;
    }

    public async Task<LoginResponseDto> Handle(
        LoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _uow.Users.GetByUsernameAsync(request.Dto.Username);

        if (user is null ||
            !BCrypt.Net.BCrypt.Verify(request.Dto.Password, user.PasswordHash))
            throw new UnauthorizedException("Credenciales invalidas.");

        var roles = user.UserRoles
            .Select(r => r.Role.RoleName)
            .ToList();

        var token = _token.GenerateToken(user);

        return new LoginResponseDto(
            token,
            user.Username,
            roles);
    }
}
