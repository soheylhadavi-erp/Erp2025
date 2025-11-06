using Common.Application;
using General.Application.Auth.Users;

namespace General.Application.Auth.Roles
{
    public interface IRoleService
    {
        // مدیریت نقش‌ها
        Task<OperationResultDto<RoleDto>> CreateRoleAsync(CreateRoleRequestDto request);
        Task<OperationResultDto> UpdateRoleAsync(Guid RoleId, string Description);
        Task<OperationResultDto> DeleteRoleAsync(Guid RoleId);

        // کوئری‌ها
        Task<RoleDto?> GetRoleByIdAsync(Guid RoleId);
        //Task<List<RoleDto>> GetAllRolesAsync();
        Task<PaginatedResultDto<RoleDto>> GetPaginatedRolesAsync(PaginatedRequest request);
        Task<List<UserDto>> GetUsersInRoleAsync(Guid RoleId);

        // مدیریت کاربران در نقش
        Task<OperationResultDto> AddUserToRoleAsync(Guid RoleId, Guid UserId);
        Task<OperationResultDto> RemoveUserFromRoleAsync(Guid RoleId, Guid UserId);
    }
}