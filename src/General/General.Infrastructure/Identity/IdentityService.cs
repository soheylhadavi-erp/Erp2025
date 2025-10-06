using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Identity;

public class IdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtService _jwtService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<(bool Success, string? Token, string? Error)> RegisterAsync(string email, string password, string fullName)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return (false, null, string.Join(", ", result.Errors.Select(e => e.Description)));

        return (true, _jwtService.GenerateToken(user.Id, user.Email!), null);
    }

    public async Task<(bool Success, string? Token, string? Error)> LoginAsync(string email, string password)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return (false, null, "User not found");

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            return (false, null, "Invalid credentials");

        var roles = await _userManager.GetRolesAsync(user);
        return (true, _jwtService.GenerateToken(user.Id, user.Email!, roles), null);
    }
}
