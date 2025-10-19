// Application/Interfaces/IIdentityService.cs
using General.Application.Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace General.Application.Interfaces.Auth
{
    public interface IIdentityService
    {
        Task<AuthResultDto> RegisterAsync(string email, string password, string fullName);
        Task<AuthResultDto> LoginAsync(string email, string password);
    }
}
