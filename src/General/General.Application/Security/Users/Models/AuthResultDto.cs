using Common.Application.Models;
using General.Application.Users;

namespace General.Application.Auth.Users.Models
{
    public class AuthResultDto : ResultDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserDto User { get; set; }
    }
}
