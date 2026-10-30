using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Authentication;

public class JwtTokenService : IJwtTokenService
{
    public const string CookieName = "access_token";

    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(User user, bool rememberMe)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetRequired("Jwt:Key")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.Name, user.DisplayName),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: GetRequired("Jwt:Issuer"),
            audience: GetRequired("Jwt:Audience"),
            claims: claims,
            expires: GetExpiry(rememberMe).UtcDateTime,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public DateTimeOffset GetExpiry(bool rememberMe)
    {
        if (rememberMe)
        {
            return DateTimeOffset.UtcNow.AddDays(7);
        }

        var minutes = int.TryParse(_configuration["Jwt:ExpiryMinutes"], out var value) ? value : 60;
        return DateTimeOffset.UtcNow.AddMinutes(minutes);
    }

    private string GetRequired(string key)
    {
        return _configuration[key] ?? throw new InvalidOperationException($"تنظیم {key} پیدا نشد.");
    }
}
