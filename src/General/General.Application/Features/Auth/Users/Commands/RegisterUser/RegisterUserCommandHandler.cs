using MediatR;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Domain.Entities;
using General.Application.Auth.Profiles; // یا مسیر پروفایل
using General.Contract.Users;
using General.Application.Interfaces.Auth;

namespace Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper,IIdentityService identityService)
        {
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // بررسی وجود کاربر
            var existingUser = await _identityService.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return new RegisterUserResponse { Success = false, Message = "این ایمیل قبلاً ثبت شده است." };

            // استفاده از AutoMapper برای تبدیل Command به Entity
            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _identityService.CreateAsync(user, request.Password);

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
