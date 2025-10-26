namespace General.Infrastructure.Security.Services
{
    using General.Application.Auth.Users.Models;
    using General.Application.Security.Roles.Interfaces;
    using General.Application.Security.Roles.Models;
    using General.Infrastructure.Data;
    using General.Infrastructure.Security.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    namespace Infrastructure.Identity.Services
    {
        public class RoleService : IRoleService
        {
            private readonly RoleManager<ApplicationRole> _roleManager;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly AppDbContext _context;
            private readonly ILogger<RoleService> _logger;

            public RoleService(
                RoleManager<ApplicationRole> roleManager,
                UserManager<ApplicationUser> userManager,
                AppDbContext context,
                ILogger<RoleService> logger)
            {
                _roleManager = roleManager;
                _userManager = userManager;
                _context = context;
                _logger = logger;
            }

            public async Task<RoleOperationResult> CreateRoleAsync(CreateRoleRequest request)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(request.Name))
                        return RoleOperationResult.Failure("Role name cannot be empty");

                    if (await _roleManager.RoleExistsAsync(request.Name))
                        return RoleOperationResult.Failure("Role name already exists");

                    var role = new ApplicationRole
                    {
                        Name = request.Name,
                        Description = request.Description
                    };

                    var result = await _roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                        return RoleOperationResult.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("Role {RoleName} created successfully", request.Name);

                    var roleDto = new RoleDto { Id = role.Id, Name = role.Name, Description = role.Description };
                    return RoleOperationResult.Success("Role created successfully", roleDto);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating role {RoleName}", request.Name);
                    return RoleOperationResult.Failure("Error creating role");
                }
            }

            public async Task<RoleOperationResult> UpdateRoleAsync(UpdateRoleRequest request)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                    if (role == null)
                        return RoleOperationResult.Failure("Role not found");

                    // جلوگیری از تغییر نقش‌های سیستمی
                    if (IsSystemRole(role.Name))
                        return RoleOperationResult.Failure("Cannot modify system role");

                    if (!string.IsNullOrWhiteSpace(request.Description))
                        role.Description = request.Description;

                    var result = await _roleManager.UpdateAsync(role);
                    if (!result.Succeeded)
                        return RoleOperationResult.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("Role {RoleId} updated successfully", request.RoleId);
                    return RoleOperationResult.Success("Role updated successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating role {RoleId}", request.RoleId);
                    return RoleOperationResult.Failure("Error updating role");
                }
            }

            public async Task<RoleOperationResult> DeleteRoleAsync(DeleteRoleRequest request)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                    if (role == null)
                        return RoleOperationResult.Failure("Role not found");

                    // جلوگیری از حذف نقش‌های سیستمی
                    if (IsSystemRole(role.Name))
                        return RoleOperationResult.Failure("Cannot delete system role");

                    // بررسی اینکه آیا کاربرانی با این نقش وجود دارند
                    var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                    if (usersInRole.Any())
                        return RoleOperationResult.Failure("Cannot delete role that has assigned users");

                    var result = await _roleManager.DeleteAsync(role);
                    if (!result.Succeeded)
                        return RoleOperationResult.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("Role {RoleId} deleted successfully", request.RoleId);
                    return RoleOperationResult.Success("Role deleted successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting role {RoleId}", request.RoleId);
                    return RoleOperationResult.Failure("Error deleting role");
                }
            }

            public async Task<RoleDto?> GetRoleByIdAsync(GetRoleByIdRequest request)
            {
                return await _roleManager.Roles
                    .Where(r => r.Id == request.RoleId)
                    .Select(r => new RoleDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description
                    })
                    .FirstOrDefaultAsync();
            }

            public async Task<List<RoleDto>> GetAllRolesAsync()
            {
                return await _roleManager.Roles
                    .Select(r => new RoleDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description
                    })
                    .ToListAsync();
            }

            public async Task<List<UserDto>> GetUsersInRoleAsync(GetUsersInRoleRequest request)
            {
                var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                if (role == null)
                    return new List<UserDto>();

                var users = await _userManager.GetUsersInRoleAsync(role.Name);

                return users.Select(u => new UserDto
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FullName = u.FullName,
                    IsActive=u.IsActive,
                }).ToList();
            }

            public async Task<RoleOperationResult> AddUserToRoleAsync(AddUserToRoleRequest request)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                    if (role == null)
                        return RoleOperationResult.Failure("Role not found");

                    var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                    if (user == null)
                        return RoleOperationResult.Failure("User not found");

                    // بررسی اینکه آیا کاربر از قبل این نقش را دارد
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                        return RoleOperationResult.Failure("User already has this role");

                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                        return RoleOperationResult.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("User {UserId} added to role {RoleName}", request.RoleId, role.Name);
                    return RoleOperationResult.Success("User added to role successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding user {UserId} to role {RoleId}", request.UserId, request.RoleId);
                    return RoleOperationResult.Failure("Error adding user to role");
                }
            }

            public async Task<RoleOperationResult> RemoveUserFromRoleAsync(RemoveUserFromRoleRequest request)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                    if (role == null)
                        return RoleOperationResult.Failure("Role not found");

                    var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                    if (user == null)
                        return RoleOperationResult.Failure("User not found");

                    // بررسی اینکه آیا کاربر این نقش را دارد
                    if (!await _userManager.IsInRoleAsync(user, role.Name))
                        return RoleOperationResult.Failure("User does not have this role");

                    var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                        return RoleOperationResult.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("User {UserId} removed from role {RoleName}", request.UserId, role.Name);
                    return RoleOperationResult.Success("User removed from role successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error removing user {UserId} from role {RoleId}", request.UserId, request.RoleId);
                    return RoleOperationResult.Failure("Error removing user from role");
                }
            }

            private bool IsSystemRole(string roleName)
            {
                var systemRoles = new[] { "SuperAdmin", "Admin", "User" };
                return systemRoles.Contains(roleName);
            }
        }
    }
}
