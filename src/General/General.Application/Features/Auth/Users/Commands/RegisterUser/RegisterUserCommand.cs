using MediatR;
using General.Contract.Users;
namespace General.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
