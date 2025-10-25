using Common.Application.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Auth.Users.Models
{
    public class AuthResultDto:ResultDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserDto User { get; set; }
    }
}
