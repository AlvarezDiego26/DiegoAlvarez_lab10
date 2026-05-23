using DiegoAlvarez.Domain.Entities;

namespace DiegoAlvarez.Application.Interfaces.Security;

public interface ITokenService
{
    string GenerateToken(User user);
}
