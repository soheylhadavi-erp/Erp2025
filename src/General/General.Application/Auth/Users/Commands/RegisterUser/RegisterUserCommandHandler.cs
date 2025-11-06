using AutoMapper;
using General.Contract.Auth.Users;
using MediatR;

namespace General.Application.Auth.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserService IUserService;

        public RegisterUserCommandHandler(IMapper mapper, IUserService identityService)
        {
            _mapper = mapper;
            IUserService = identityService;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await IUserService.RegisterAsync(request.Email, request.Password, request.FullName);

            if (!result.Success)
                return new RegisterUserResponse
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors)
                };

            return new RegisterUserResponse
            {
                Success = true,
                Message = "ثبت‌ نام با موفقیت انجام شد."
            };

        }
    }
}
