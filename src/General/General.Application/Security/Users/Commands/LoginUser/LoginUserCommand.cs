using MediatR;
using General.Contract.Users;
namespace General.Application.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
