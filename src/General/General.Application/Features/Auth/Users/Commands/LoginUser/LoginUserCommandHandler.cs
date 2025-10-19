using MediatR;
using General.Contract.Users;
using General.Application.Interfaces.Auth;

namespace General.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IIdentityService _identityService;

        public LoginUserCommandHandler(IIdentityService identityService)
        {
            //_userManager = userManager;
            _identityService = identityService;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.LoginAsync(request.Email,request.Password);
            if (!result.Success)
            {
                return new LoginUserResponse
                {
                    Success = false,
                    Token = "",
                    Message = result.Errors[0]
                };
            }

            return new LoginUserResponse
            {
                Success = true,
                Token =result.Token,
                Message = "ورود موفقیت‌آمیز بود."
            };
        }
    }
}
