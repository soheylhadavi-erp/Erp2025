using General.Infrastructure.Data;
using General.Infrastructure.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using General.Application.Auth.Permissions.Interfaces;
using General.Application.Auth.Permissions.Models;
namespace General.Infrastructure.Security.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<PermissionService> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // User
        public async Task<bool> UserHasPermissionAsync(Guid userId, string permissionName)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null) return false;

                // Checking user permissions directly
                var hasDirectPermission = await _context.UserPermissions
                    .Include(up => up.Permission)
                    .AnyAsync(up => up.UserId == userId && up.Permission.Name == permissionName);

                if (hasDirectPermission) return true;

                // Checking permissions through roles

                var userRoles = await _userManager.GetRolesAsync(user);
                var hasRolePermission = await _context.RolePermissions
                    .Include(rp => rp.Role)
                    .Include(rp => rp.Permission)
                    .AnyAsync(rp => userRoles.Contains(rp.Role.Name) && rp.Permission.Name == permissionName);

                return hasRolePermission;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking user permission for user {userId} and permission {permissionName}", userId, permissionName);
                return false;
            }
        }

        public async Task<List<PermissionDto>> GetUserPermissionsAsync(Guid userId)
        {
            var directPermissions = await GetUserDirectPermissionsAsync(userId);
            var rolePermissions = await GetUserRolePermissionsAsync(userId);

            // Merge and remove duplicates
            return directPermissions
                .Union(rolePermissions, new PermissionDtoComparer())
                .ToList();
        }

        public async Task<List<PermissionDto>> GetUserDirectPermissionsAsync(Guid userId)
        { 
            return await _context.UserPermissions
                .Where(up => up.UserId == userId)
                .Include(up => up.Permission)
                .ThenInclude(p => p.Category)
                .Select(up => new PermissionDto
                {
                    Id = up.Permission.Id,
                    Name = up.Permission.Name,
                    Description = up.Permission.Description,
                    Category = up.Permission.Category.Name,
                    CategoryId = up.Permission.CategoryId,
                    IsDirect = true
                })
                .ToListAsync();
        }

        public async Task<List<PermissionDto>> GetUserRolePermissionsAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return new List<PermissionDto>();

            var userRoles = await _userManager.GetRolesAsync(user);

            return await _context.RolePermissions
                .Where(rp => userRoles.Contains(rp.Role.Name))
                .Include(rp => rp.Permission)
                .ThenInclude(p => p.Category)
                .Select(rp => new PermissionDto
                {
                    Id = rp.Permission.Id,
                    Name = rp.Permission.Name,
                    Description = rp.Permission.Description,
                    Category = rp.Permission.Category.Name,
                    CategoryId = rp.Permission.CategoryId,
                    IsDirect = false
                })
                .Distinct()
                .ToListAsync();
        }

        public async Task<AssignPermissionResult> AssignPermissionToUserAsync(Guid userId, Guid permissionId)
        {
            try
            {
                // Checking for user existence
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    return AssignPermissionResult.Failure("User not found");

                // Checking for the existence of permissions
                var permission = await _context.SystemPermissions.FindAsync(permissionId);
                if (permission == null)
                    return AssignPermissionResult.Failure("Permission not found");

                // Check for non-duplicates
                var existingPermission = await _context.UserPermissions
                    .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId);

                if (existingPermission != null)
                    return AssignPermissionResult.Failure("Permission already assigned to user");

                // Permission assignment
                var userPermission = new UserPermission
                {
                    UserId = userId,
                    PermissionId = permissionId
                };

                _context.UserPermissions.Add(userPermission);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Permission {PermissionId} assigned to user {UserId}", permissionId, userId);
                return AssignPermissionResult.Success("Permission assigned successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error assigning permission {permissionId} to user {userId}", permissionId, userId);
                return AssignPermissionResult.Failure("Error assigning permission");
            }
        }

        public async Task<AssignPermissionResult> RemovePermissionFromUserAsync(Guid userId, Guid permissionId)
        {
            try
            {
                var userPermission = await _context.UserPermissions
                    .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId);

                if (userPermission == null)
                    return AssignPermissionResult.Failure("Permission not assigned to user");

                _context.UserPermissions.Remove(userPermission);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Permission {PermissionId} removed from user {UserId}", permissionId, userId);
                return AssignPermissionResult.Success("Permission removed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing permission {permissionId} from user {userId}", permissionId, userId);
                return AssignPermissionResult.Failure("Error removing permission");
            }
        }

        // Role
        public async Task<bool> RoleHasPermissionAsync(Guid roleId, string permissionName)
        {
            return await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .AnyAsync(rp => rp.RoleId == roleId && rp.Permission.Name == permissionName);
        }

        public async Task<List<PermissionDto>> GetRolePermissionsAsync(Guid roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .ThenInclude(p => p.Category)
                .Select(rp => new PermissionDto
                {
                    Id = rp.Permission.Id,
                    Name = rp.Permission.Name,
                    Description = rp.Permission.Description,
                    Category = rp.Permission.Category.Name,
                    CategoryId = rp.Permission.CategoryId
                })
                .ToListAsync();
        }

        public async Task<AssignPermissionResult> AssignPermissionToRoleAsync(Guid roleId, Guid permissionId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                    return AssignPermissionResult.Failure("Role not found");

                var permission = await _context.SystemPermissions.FindAsync(permissionId);
                if (permission == null)
                    return AssignPermissionResult.Failure("Permission not found");

                var existingPermission = await _context.RolePermissions
                    .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

                if (existingPermission != null)
                    return AssignPermissionResult.Failure("Permission already assigned to role");

                var rolePermission = new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                };

                _context.RolePermissions.Add(rolePermission);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Permission {PermissionId} assigned to role {roleId}", permissionId, roleId);
                return AssignPermissionResult.Success("Permission assigned to role successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error assigning permission {permissionId} to role {roleId}", permissionId, roleId);
                return AssignPermissionResult.Failure("Error assigning permission to role");
            }
        }

        public async Task<AssignPermissionResult> AssignPermissionsToRoleAsync(Guid roleId, List<Guid> permissionIds)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                    return AssignPermissionResult.Failure("Role not found");

                // بررسیChecking for the existence of all permissions
                var existingPermissions = await _context.SystemPermissions
                    .Where(p => permissionIds.Contains(p.Id))
                    .Select(p => p.Id)
                    .ToListAsync();

                var invalidPermissions = permissionIds.Except(existingPermissions).ToList();
                if (invalidPermissions.Any())
                    return AssignPermissionResult.Failure($"Invalid permission IDs: {string.Join(", ", invalidPermissions)}");

                // Remove current permissions
                var currentPermissions = await _context.RolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .ToListAsync();

                _context.RolePermissions.RemoveRange(currentPermissions);

                // Adding new permissions
                foreach (var permissionId in permissionIds)
                {
                    _context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    });
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"{permissionIds.Count} permissions assigned to role {roleId}", permissionIds.Count, roleId);
                return AssignPermissionResult.Success($"{permissionIds.Count} permissions assigned to role successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error assigning permissions to role {roleId}", roleId);
                return AssignPermissionResult.Failure("Error assigning permissions to role");
            }
        }

        public async Task<AssignPermissionResult> RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            try
            {
                var rolePermission = await _context.RolePermissions
                    .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

                if (rolePermission == null)
                    return AssignPermissionResult.Failure("Permission not assigned to role");

                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Permission {permissionId} removed from role {roleId}", permissionId, roleId);
                return AssignPermissionResult.Success("Permission removed from role successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing permission {permissionId} from role {roleId}", permissionId, roleId);
                return AssignPermissionResult.Failure("Error removing permission from role");
            }
        }

        public async Task<AssignPermissionResult> UpdateRolePermissionsAsync(Guid roleId, List<Guid> permissionIds)
        {
            return await AssignPermissionsToRoleAsync(roleId, permissionIds);
        }

        // System permissions
        public async Task<List<PermissionCategoryDto>> GetGroupedPermissionsAsync()
        {
            return await _context.PermissionCategories
                .Include(pc => pc.Permissions)
                .OrderBy(pc => pc.DisplayOrder)
                .Select(pc => new PermissionCategoryDto
                {
                    Id = pc.Id,
                    Name = pc.Name,
                    Description = pc.Description,
                    DisplayOrder = pc.DisplayOrder,
                    Permissions = pc.Permissions.Select(p => new PermissionDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Category = pc.Name,
                        CategoryId = pc.Id
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<PermissionDto>> GetAllPermissionsAsync()
        {
            return await _context.SystemPermissions
                .Include(p => p.Category)
                .OrderBy(p => p.Category.DisplayOrder)
                .ThenBy(p => p.Name)
                .Select(p => new PermissionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category.Name,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(Guid permissionId)
        {
            return await _context.SystemPermissions
                .Include(p => p.Category)
                .Where(p => p.Id == permissionId)
                .Select(p => new PermissionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category.Name,
                    CategoryId = p.CategoryId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PermissionDto> GetPermissionByNameAsync(string permissionName)
        {
            return await _context.SystemPermissions
                .Include(p => p.Category)
                .Where(p => p.Name == permissionName)
                .Select(p => new PermissionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category.Name,
                    CategoryId = p.CategoryId
                })
                .FirstOrDefaultAsync();
        }

        // Check access
        public async Task<bool> CheckUserAccessAsync(Guid userId, string permissionName)
        {
            return await UserHasPermissionAsync(userId, permissionName);
        }

        public async Task<List<string>> GetUserPermissionNamesAsync(Guid userId)
        {
            var permissions = await GetUserPermissionsAsync(userId);
            return permissions.Select(p => p.Name).ToList();
        }
    }

    // Helper class for comapring PermissionDto
    public class PermissionDtoComparer : IEqualityComparer<PermissionDto>
    {
        public bool Equals(PermissionDto x, PermissionDto y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;

            return x.Id == y.Id; 
        }

        public int GetHashCode(PermissionDto obj)
        {
            if (obj is null) return 0;
            return obj.Id.GetHashCode(); 
        }
    }
}