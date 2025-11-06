using General.Application.Auth.Permissions;
using General.Application.Auth.Users;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace General.Infrastructure.Auth.Users
{
    public class ClaimService : IClaimService
    {
        private readonly IPermissionService _permissionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimService(IPermissionService permissionService, UserManager<ApplicationUser> userManager)
        {
            _permissionService = permissionService;
            _userManager = userManager;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(Guid userId)
        {
            var claims = new List<Claim>();

            // ۱. Claims پایه با JwtRegisteredClaimNames
            claims.AddRange(GetBasicClaims(userId));

            // ۲. نقش‌های کاربر
            var roles = await GetUserRolesAsync(userId);
            claims.AddRange(roles);

            // ۳. پرمیژن‌های کاربر
            var permissions = await GetUserPermissionsAsClaimsAsync(userId);
            claims.AddRange(permissions);

            // ۴. Claims اضافی از Identity
            var additionalClaims = await GetAdditionalUserClaimsAsync(userId);
            claims.AddRange(additionalClaims);

            return claims;
        }

        private List<Claim> GetBasicClaims(Guid userId)
        {
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;

            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user?.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.UniqueName, user?.UserName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, user?.UserName ?? ""),
                new Claim("full_name", user?.FullName ?? "")
            };
        }

        private async Task<List<Claim>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return new List<Claim>();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
        }

        private async Task<List<Claim>> GetUserPermissionsAsClaimsAsync(Guid userId)
        {
            var permissions = await _permissionService.GetUserPermissionNamesAsync(userId);
            return permissions.Select(permission => new Claim("permission", permission)).ToList();
        }

        private async Task<List<Claim>> GetAdditionalUserClaimsAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return new List<Claim>();

            return (List<Claim>)await _userManager.GetClaimsAsync(user);
        }
    }
}