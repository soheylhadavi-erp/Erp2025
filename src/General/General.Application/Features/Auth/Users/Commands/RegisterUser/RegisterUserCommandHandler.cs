using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using General.Contract.Users;
namespace Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // بررسی وجود کاربر
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return new RegisterUserResponse { Success = false, Message = "این ایمیل قبلاً ثبت شده است." };

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                NationalCode = request.NationalCode
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new RegisterUserResponse
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };

            return new RegisterUserResponse
            {
                Success = true,
                Message = "ثبت‌نام با موفقیت انجام شد."
            };
        }
    }
}
