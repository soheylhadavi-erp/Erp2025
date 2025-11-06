namespace General.Application.Auth.Users
{
    public interface IUserService
    {
        Task<AuthResultDto> RegisterAsync(string email, string password, string fullName);
        Task<AuthResultDto> LoginAsync(string email, string password);
    }
}
