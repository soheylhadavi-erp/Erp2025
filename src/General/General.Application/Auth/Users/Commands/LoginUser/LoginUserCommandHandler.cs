using General.Contract.Auth.Users;
using MediatR;

namespace General.Application.Auth.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService identityService)
        {
            //_userManager = userManager;
            _userService = identityService;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(request.Email, request.Password);
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
                Token = result.Token,
                Message = "ورود موفقیت‌آمیز بود."
            };

        }
    }
}
