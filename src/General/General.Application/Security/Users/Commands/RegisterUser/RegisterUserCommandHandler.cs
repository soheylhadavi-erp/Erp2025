using MediatR;
using AutoMapper;

using General.Contract.Users;
using General.Application.Auth.Users.Interfaces;

namespace General.Application.Auth.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IMapper mapper,IIdentityService identityService)
        {
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RegisterAsync(request.Email, request.Password,request.FullName);

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
