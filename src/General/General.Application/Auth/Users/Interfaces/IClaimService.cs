using System.Security.Claims;

namespace General.Application.Auth.Users
{
    public interface IClaimService
    {
        Task<List<Claim>> GetUserClaimsAsync(Guid userId);
    }
}
