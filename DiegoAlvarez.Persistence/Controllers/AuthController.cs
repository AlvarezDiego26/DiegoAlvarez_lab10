using DiegoAlvarez.Application.DTOs.Auth;
using DiegoAlvarez.Application.DTOs.Common;
using DiegoAlvarez.Application.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DiegoAlvarez.Persistence.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserUseCase _register;
    private readonly LoginUseCase _login;

    public AuthController(
        RegisterUserUseCase register,
        LoginUseCase login)
    {
        _register = register;
        _login = login;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        try
        {
            var result = await _register.ExecuteAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(
                new MessageResponseDto(ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        try
        {
            var result = await _login.ExecuteAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(
                new MessageResponseDto(ex.Message));
        }
    }
}
