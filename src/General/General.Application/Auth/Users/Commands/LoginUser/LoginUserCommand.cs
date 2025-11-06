using General.Contract.Auth.Users;
using MediatR;
namespace General.Application.Auth.Users
{
    public class LoginUserCommand : IRequest<LoginUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
