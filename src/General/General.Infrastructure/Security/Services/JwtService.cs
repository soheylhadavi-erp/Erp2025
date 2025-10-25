using General.Application.Security.Users.Interfaces;
using General.Infrastructure.Security.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.Security.Services;

public class JwtService
{
    private readonly JwtSettings _settings;
    private readonly IClaimService _claimService;

    public JwtService(IOptions<JwtSettings> settings,IClaimService claimService)
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
