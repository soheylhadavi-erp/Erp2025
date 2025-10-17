using Common.Application.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Models.Auth
{
    public class AuthResultDto:ResultDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
