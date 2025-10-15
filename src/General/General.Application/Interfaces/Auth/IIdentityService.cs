// Application/Interfaces/IIdentityService.cs
using System.Collections.Generic;
using System.Threading.Tasks;

namespace General.Application.Interfaces.Auth
{
    public interface IIdentityService
    {
        Task<(bool Success, string? Token, string? Error)> RegisterAsync(string email, string password, string fullName);
        Task<(bool Success, string? Token, string? Error)> LoginAsync(string email, string password);
    }
}
