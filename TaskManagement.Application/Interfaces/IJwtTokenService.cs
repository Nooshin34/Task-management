using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces;

public interface IJwtTokenService
{
    string CreateToken(User user, bool rememberMe);
    DateTimeOffset GetExpiry(bool rememberMe);
}
