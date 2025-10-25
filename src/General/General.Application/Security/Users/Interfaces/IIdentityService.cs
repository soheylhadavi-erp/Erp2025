// Application/Interfaces/IIdentityService.cs
using General.Application.Auth.Users.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace General.Application.Auth.Users.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthResultDto> RegisterAsync(string email, string password, string fullName);
        Task<AuthResultDto> LoginAsync(string email, string password);
    }
}
