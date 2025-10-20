using General.Application.Interfaces.Auth;
using General.Application.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace General.Infrastructure.Identity;

public class IdentityService: IIdentityService
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
    [Authorize]
    public async Task<AuthResultDto> RegisterAsync(string email, string password, string fullName)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return new AuthResultDto()
            {
                Errors = result.Errors.Select(e => e.Description).ToArray(),
                Success = false,
                Token = ""
            }; 

        return new AuthResultDto()
        {
            Errors = [],
            Success = true,
            Token = _jwtService.GenerateToken(user.Id, user.Email!)
        }; 
    }
    [AllowAnonymous]
    public async Task<AuthResultDto> LoginAsync(string email, string password)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return new AuthResultDto()
            {
                Errors = new string[] { "User not found" },
                Success=false,
                Token = ""
            };

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            return new AuthResultDto()
            {
                Errors = new string[] { "Invalid credentials" },
                Success = false,
                Token = ""
            };

        var roles = await _userManager.GetRolesAsync(user);
        return new AuthResultDto()
        {
            Errors = [],
            Success = true,
            Token = _jwtService.GenerateToken(user.Id, user.Email!, roles)
        };
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
