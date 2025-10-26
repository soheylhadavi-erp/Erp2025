using Common.Application.Models.Auth;

namespace General.Application.Auth.Users.Models
{
    public class AuthResultDto : ResultDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserDto User { get; set; }
    }
}
