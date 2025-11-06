using General.Contract.Auth.Users;
using MediatR;
namespace General.Application.Auth.Users
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
