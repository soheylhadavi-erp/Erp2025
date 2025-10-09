using MediatR;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Domain.Entities;
using General.Application.Auth.Profiles; // یا مسیر پروفایل
using General.Contract.Users;

namespace Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // بررسی وجود کاربر
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return new RegisterUserResponse { Success = false, Message = "این ایمیل قبلاً ثبت شده است." };

            // استفاده از AutoMapper برای تبدیل Command به Entity
            var user = _mapper.Map<ApplicationUser>(request);

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
                Message = "ثبت‌ نام با موفقیت انجام شد."
            };

            return response;
        }
    }
}
