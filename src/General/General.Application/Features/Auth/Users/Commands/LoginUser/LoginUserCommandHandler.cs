using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using General.Contract.Users;
using General.Application.Interfaces.Auth;

namespace General.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityService _identityService;
        //private readonly IConfiguration _configuration;

        public LoginUserCommandHandler(IIdentityService identityService /*UserManager<ApplicationUser> userManager*/, IConfiguration configuration)
        {
            //_userManager = userManager;
            _identityService = identityService;
            _configuration = configuration;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            //var user = await _identityService.LoginAsync(request.Email,request.Password);
            var result = await _identityService.LoginAsync(request.Email,request.Password);
            //if (user == null)
            //    return new LoginUserResponse { Success = false, Message = "کاربر یافت نشد." };

            //var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            //if (!passwordCheck)
            //    return new LoginUserResponse { Success = false, Message = "رمز عبور اشتباه است." };

            // ایجاد توکن JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginUserResponse
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                Message = "ورود موفقیت‌آمیز بود."
            };
        }
    }
}
