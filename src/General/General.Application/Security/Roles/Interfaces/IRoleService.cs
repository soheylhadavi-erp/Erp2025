using Common.Application.Models;
using General.Application.Auth.Users.Models;
using General.Application.Security.Roles.Models;

namespace General.Application.Security.Roles.Interfaces
{
    public interface IRoleService
    {
        // مدیریت نقش‌ها
        Task<CreateResultDto> CreateRoleAsync(CreateRoleRequestDto request);
        Task<OperationResultDto> UpdateRoleAsync(Guid RoleId, string Description);
        Task<OperationResultDto> DeleteRoleAsync(Guid RoleId);

        // کوئری‌ها
        Task<RoleDto?> GetRoleByIdAsync(Guid RoleId);
        Task<List<RoleDto>> GetAllRolesAsync();
        Task<List<UserDto>> GetUsersInRoleAsync(Guid RoleId);

        // مدیریت کاربران در نقش
        Task<OperationResultDto> AddUserToRoleAsync(Guid RoleId , Guid UserId);
        Task<OperationResultDto> RemoveUserFromRoleAsync(Guid RoleId, Guid UserId);
    }
}