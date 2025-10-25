using General.Application.Auth.Permissions.Models;

namespace General.Application.Auth.Permissions.Interfaces;

public interface IPermissionService
{
    // کاربر
    Task<bool> UserHasPermissionAsync(Guid userId, string permissionName);
    Task<List<PermissionDto>> GetUserPermissionsAsync(Guid userId);
    Task<List<PermissionDto>> GetUserDirectPermissionsAsync(Guid userId);
    Task<List<PermissionDto>> GetUserRolePermissionsAsync(Guid userId);
    Task<AssignPermissionResult> AssignPermissionToUserAsync(Guid userId, Guid permissionId);
    Task<AssignPermissionResult> RemovePermissionFromUserAsync(Guid userId, Guid permissionId);

    // نقش
    Task<bool> RoleHasPermissionAsync(Guid roleId, string permissionName);
    Task<List<PermissionDto>> GetRolePermissionsAsync(Guid roleId);
    Task<AssignPermissionResult> AssignPermissionToRoleAsync(Guid roleId, Guid permissionId);
    Task<AssignPermissionResult> AssignPermissionsToRoleAsync(Guid roleId, List<Guid> permissionIds);
    Task<AssignPermissionResult> RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId);
    Task<AssignPermissionResult> UpdateRolePermissionsAsync(Guid roleId, List<Guid> permissionIds);

    // پرمیژن‌های سیستم
    Task<List<PermissionCategoryDto>> GetGroupedPermissionsAsync();
    Task<List<PermissionDto>> GetAllPermissionsAsync();
    Task<PermissionDto> GetPermissionByIdAsync(Guid permissionId);
    Task<PermissionDto> GetPermissionByNameAsync(string permissionName);

    // بررسی دسترسی
    Task<bool> CheckUserAccessAsync(Guid userId, string permissionName);
    Task<List<string>> GetUserPermissionNamesAsync(Guid userId);
}

public class AssignPermissionResult
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public static AssignPermissionResult Success(string message = "Operation completed successfully")
    {
        return new AssignPermissionResult { Succeeded = true, Message = message };
    }

    public static AssignPermissionResult Failure(string error)
    {
        return new AssignPermissionResult { Succeeded = false, Errors = new List<string> { error } };
    }

    public static AssignPermissionResult Failure(List<string> errors)
    {
        return new AssignPermissionResult { Succeeded = false, Errors = errors };
    }
}