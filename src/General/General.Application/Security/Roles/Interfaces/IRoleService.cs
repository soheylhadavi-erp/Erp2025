using General.Application.Auth.Users.Models;
using General.Application.Security.Roles.Models;

namespace General.Application.Security.Roles.Interfaces
{
    public interface IRoleService
    {
        // مدیریت نقش‌ها
        Task<RoleOperationResult> CreateRoleAsync(CreateRoleRequest request);
        Task<RoleOperationResult> UpdateRoleAsync(UpdateRoleRequest request);
        Task<RoleOperationResult> DeleteRoleAsync(DeleteRoleRequest request);

        // کوئری‌ها
        Task<RoleDto?> GetRoleByIdAsync(GetRoleByIdRequest request);
        Task<List<RoleDto>> GetAllRolesAsync();
        Task<List<UserDto>> GetUsersInRoleAsync(GetUsersInRoleRequest request);

        // مدیریت کاربران در نقش
        Task<RoleOperationResult> AddUserToRoleAsync(AddUserToRoleRequest request);
        Task<RoleOperationResult> RemoveUserFromRoleAsync(RemoveUserFromRoleRequest request);
    }
}