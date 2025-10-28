namespace General.Infrastructure.Security.Services
{
    using Common.Application.Models;
    using General.Application.Auth.Permissions.Models;
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

            public async Task<OperationResultDto<RoleDto>> CreateRoleAsync(CreateRoleRequestDto request)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(request.Name))
                        return OperationResultDto<RoleDto>.Failure("Role name cannot be empty");

                    if (await _roleManager.RoleExistsAsync(request.Name))
                        return OperationResultDto<RoleDto>.Failure("Role name already exists");

                    var role = new ApplicationRole
                    {
                        Name = request.Name,
                        Description = request.Description
                    };

                    var result = await _roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                        return OperationResultDto<RoleDto>.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("Role {RoleName} created successfully", request.Name);

                    var roleDto = new RoleDto { Id = role.Id, Name = role.Name, Description = role.Description };
                    return OperationResultDto<RoleDto>.Success(roleDto,"Role created successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating role {RoleName}", request.Name);
                    return OperationResultDto<RoleDto>.Failure("Error creating role");
                }
            }

            public async Task<OperationResultDto> UpdateRoleAsync(Guid RoleId, string Description)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                    if (role == null)
                        return OperationResultDto.Failure("Role not found");

                    // جلوگیری از تغییر نقش‌های سیستمی
                    if (IsSystemRole(role.Name))
                        return OperationResultDto.Failure("Cannot modify system role");

                    if (!string.IsNullOrWhiteSpace(Description))
                        role.Description = Description;

                    var result = await _roleManager.UpdateAsync(role);
                    if (!result.Succeeded)
                        return OperationResultDto.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("Role {RoleId} updated successfully", RoleId);
                    return OperationResultDto.Success("Role updated successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating role {RoleId}", RoleId);
                    return OperationResultDto.Failure("Error updating role");
                }
            }

            public async Task<OperationResultDto> DeleteRoleAsync(Guid RoleId)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                    if (role == null)
                        return OperationResultDto.Failure("Role not found");

                    // جلوگیری از حذف نقش‌های سیستمی
                    if (IsSystemRole(role.Name))
                        return OperationResultDto.Failure("Cannot delete system role");

                    // بررسی اینکه آیا کاربرانی با این نقش وجود دارند
                    var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                    if (usersInRole.Any())
                        return OperationResultDto.Failure("Cannot delete role that has assigned users");

                    var result = await _roleManager.DeleteAsync(role);
                    if (!result.Succeeded)
                        return OperationResultDto.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("Role {RoleId} deleted successfully", RoleId);
                    return OperationResultDto.Success("Role deleted successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting role {RoleId}", RoleId);
                    return OperationResultDto.Failure("Error deleting role");
                }
            }

            public async Task<RoleDto?> GetRoleByIdAsync(Guid RoleId)
            {
                return await _roleManager.Roles
                    .Where(r => r.Id == RoleId)
                    .Include(x=>x.Permissions).ThenInclude(c=>c.Category)
                    .Select(r => new RoleDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Permissions=r.Permissions.Select(p=>new PermissionDto 
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Category = p.Category.Name,
                            CategoryId = p.CategoryId,
                            IsDirect = false,
                            IsAssigned=true
                        }).ToList() 
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

            public async Task<List<UserDto>> GetUsersInRoleAsync(Guid RoleId/*, GetUsersInRoleRequestDto request*/)
            {
                var role = await _roleManager.FindByIdAsync(RoleId.ToString());
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

            public async Task<OperationResultDto> AddUserToRoleAsync(Guid RoleId,Guid UserId)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                    if (role == null)
                        return OperationResultDto.Failure("Role not found");

                    var user = await _userManager.FindByIdAsync(UserId.ToString());
                    if (user == null)
                        return OperationResultDto.Failure("User not found");

                    // بررسی اینکه آیا کاربر از قبل این نقش را دارد
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                        return OperationResultDto.Failure("User already has this role");

                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                        return OperationResultDto.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("User {UserId} added to role {RoleName}", RoleId, role.Name);
                    return OperationResultDto.Success("User added to role successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding user {UserId} to role {RoleId}", UserId, RoleId);
                    return OperationResultDto.Failure("Error adding user to role");
                }
            }

            public async Task<OperationResultDto> RemoveUserFromRoleAsync(Guid RoleId, Guid UserId)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                    if (role == null)
                        return OperationResultDto.Failure("Role not found");

                    var user = await _userManager.FindByIdAsync(UserId.ToString());
                    if (user == null)
                        return OperationResultDto.Failure("User not found");

                    // بررسی اینکه آیا کاربر این نقش را دارد
                    if (!await _userManager.IsInRoleAsync(user, role.Name))
                        return OperationResultDto.Failure("User does not have this role");

                    var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                        return OperationResultDto.Failure(result.Errors.Select(e => e.Description).ToList());

                    _logger.LogInformation("User {UserId} removed from role {RoleName}", UserId, role.Name);
                    return OperationResultDto.Success("User removed from role successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error removing user {UserId} from role {RoleId}", UserId, RoleId);
                    return OperationResultDto.Failure("Error removing user from role");
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
