using Common.Application;

namespace General.Application.Auth.Users
{
    public class AuthResultDto : ResultDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserDto User { get; set; }
    }
}
