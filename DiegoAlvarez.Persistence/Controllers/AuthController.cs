using DiegoAlvarez.Application.DTOs.Auth;
using DiegoAlvarez.Application.Features.Auth.Commands;
using DiegoAlvarez.Application.Features.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiegoAlvarez.Persistence.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        return Ok(await _mediator.Send(new RegisterUserCommand(dto)));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        return Ok(await _mediator.Send(new LoginQuery(dto)));
    }
}
