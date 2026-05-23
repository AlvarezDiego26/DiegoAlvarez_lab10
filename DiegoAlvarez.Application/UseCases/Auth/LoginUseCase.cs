using DiegoAlvarez.Application.DTOs.Auth;
using DiegoAlvarez.Application.Interfaces.Security;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;

namespace DiegoAlvarez.Application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IUnitOfWork _uow;
    private readonly ITokenService _token;

    public LoginUseCase(IUnitOfWork uow, ITokenService token)
    {
        _uow = uow;
        _token = token;
    }

    public async Task<LoginResponseDto> ExecuteAsync(LoginRequestDto dto)
    {
        var user = await _uow.Users.GetByUsernameAsync(dto.Username);

        if (user is null ||
            !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Credenciales inválidas.");

        var roles = user.UserRoles
            .Select(r => r.Role.RoleName)
            .ToList();

        var token = _token.GenerateToken(user);

        return new LoginResponseDto(
            token,
            user.Username,
            roles
        );
    }
}
