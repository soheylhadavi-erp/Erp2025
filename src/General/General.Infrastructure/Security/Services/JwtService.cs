using General.Application.Auth.Users;
using General.Infrastructure.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace General.Infrastructure.Auth;

public class JwtService
{
    private readonly JwtSettings _settings;
    private readonly IClaimService _claimService;

    public JwtService(IOptions<JwtSettings> settings, IClaimService claimService)
    {
        _settings = settings.Value;
        _claimService = claimService;
    }

    public async Task<string> GenerateToken(Guid userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: await _claimService.GetUserClaimsAsync(userId),
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpireMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
