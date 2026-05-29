using System.Security.Claims;
using DiegoAlvarez.Application.Exceptions;
using DiegoAlvarez.Application.Interfaces.Security;

namespace DiegoAlvarez.Persistence.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(value, out var userId))
                throw new UnauthorizedException("Usuario no autenticado.");

            return userId;
        }
    }
}
