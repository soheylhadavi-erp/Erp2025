namespace General.Infrastructure.Auth.Roles
{
    using Common.Application;
    using General.Application.Auth.Permissions;
    using General.Application.Auth.Roles;
    using General.Application.Auth.Users;
    using General.Infrastructure;
    using General.Infrastructure.Auth.Roles;
    using General.Infrastructure.Auth.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

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
                        return new OperationResultDto<RoleDto>()
                        {
                            Succeeded = false,
                            Errors = new() { "Role name cannot be empty" }
                        };

                    if (await _roleManager.RoleExistsAsync(request.Name))
                        return new OperationResultDto<RoleDto>()
                        {
                            Succeeded = false,
                            Errors = new() { "Role name already exists" }
                        };

                    var role = new ApplicationRole
                    {
                        Name = request.Name,
                        Description = request.Description
                    };
                    var result = await _roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                        return new OperationResultDto<RoleDto>()
                        {
                            Errors = result.Errors.Select(e => e.Description).ToList()
                        };

                    _logger.LogInformation($"Role {request.Name} created successfully", request.Name);

                    var roleDto = new RoleDto { Id = role.Id, Name = role.Name, Description = role.Description };
                    return new OperationResultDto<RoleDto>()
                    {
                        Succeeded = true,
                        Message = "Role created successfully",
                        Data = roleDto
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating role {RoleName}", request.Name);
                    return new OperationResultDto<RoleDto>()
                    {
                        Succeeded = false,
                        Errors = new() { "Error creating role" }
                    };
                }
            }

            public async Task<OperationResultDto> UpdateRoleAsync(Guid RoleId, string Description)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                    if (role == null)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "Role not found" }
                        };
                    // جلوگیری از تغییر نقش‌های سیستمی
                    if (IsSystemRole(role.Name))
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "Cannot modify system role" }
                        };
                    if (!string.IsNullOrWhiteSpace(Description))
                        role.Description = Description;

                    var result = await _roleManager.UpdateAsync(role);
                    if (!result.Succeeded)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = result.Errors.Select(e => e.Description).ToList()
                        };

                    _logger.LogInformation($"Role {RoleId} updated successfully", RoleId);
                    return new OperationResultDto()
                    {
                        Succeeded = true,
                        Message = "Role updated successfully"
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating role {RoleId}", RoleId);
                    return new OperationResultDto()
                    {
                        Succeeded = false,
                        Errors = new() { "Error updating role" }
                    };
                }
            }

            public async Task<OperationResultDto> DeleteRoleAsync(Guid RoleId)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                    if (role == null)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "Role not found" }
                        };

                    // جلوگیری از حذف نقش‌های سیستمی
                    if (IsSystemRole(role.Name))
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "Cannot delete system role" }
                        };
                    // بررسی اینکه آیا کاربرانی با این نقش وجود دارند
                    var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                    if (usersInRole.Any())
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "Cannot delete role that has assigned users" }
                        };

                    var result = await _roleManager.DeleteAsync(role);
                    if (!result.Succeeded)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = result.Errors.Select(e => e.Description).ToList()
                        };
                    _logger.LogInformation($"Role {RoleId} deleted successfully", RoleId);
                    return new OperationResultDto()
                    {
                        Succeeded = true,
                        Message = "Role deleted successfully"
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting role {RoleId}", RoleId);
                    return new OperationResultDto()
                    {
                        Succeeded = false,
                        Errors = new() { "Error deleting role" }
                    };
                }
            }

            public async Task<RoleDto?> GetRoleByIdAsync(Guid RoleId)
            {
                return await _roleManager.Roles
                    .Where(r => r.Id == RoleId)
                    .Include(x => x.Permissions).ThenInclude(c => c.Category)
                    .Select(r => new RoleDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Permissions = r.Permissions.Select(p => new PermissionDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Category = p.Category.Name,
                            CategoryId = p.CategoryId,
                            IsDirect = false,
                            IsAssigned = true
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();
            }

            //public async Task<List<RoleDto>> GetAllRolesAsync()
            //{
            //    return await _roleManager.Roles
            //        .Select(r => new RoleDto
            //        {
            //            Id = r.Id,
            //            Name = r.Name,
            //            Description = r.Description
            //        })
            //        .ToListAsync();
            //}

            public async Task<PaginatedResultDto<RoleDto>> GetPaginatedRolesAsync(PaginatedRequest request)
            {
                // محدودیت صفحه‌بندی
                var pageSize = Math.Min(request.PageSize, PaginatedRequest.MaxPageSize);
                var pageNumber = Math.Max(request.PageNumber, 1);
                var skipRecords = Math.Max(request.SkipRecords, 0); // عدم منفی بودن

                var query = _roleManager.Roles.AsNoTracking();

                var totalCount = await query.CountAsync();

                var items = await query
                    .Select(r => new RoleDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description
                    })
                    .Skip(skipRecords) // اول SkipRecords رو رد کن
                    .Skip((pageNumber - 1) * pageSize) // سپس صفحه‌بندی کن
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedResultDto<RoleDto>
                {
                    Items = items,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    SkippedRecords = skipRecords
                };
            }

            public async Task<List<UserDto>> GetUsersInRoleAsync(Guid RoleId)
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
                    IsActive = u.IsActive,
                }).ToList();
            }

            public async Task<OperationResultDto> AddUserToRoleAsync(Guid RoleId, Guid UserId)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                    if (role == null)
                        return new() { Succeeded = false, Errors = new() { "Role not found" } };

                    var user = await _userManager.FindByIdAsync(UserId.ToString());
                    if (user == null)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "User not found" }
                        };

                    // بررسی اینکه آیا کاربر از قبل این نقش را دارد
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "User already has this role" }
                        };

                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = result.Errors.Select(e => e.Description).ToList()
                        };

                    _logger.LogInformation("User {UserId} added to role {role.Name}", RoleId, role.Name);
                    return new OperationResultDto()
                    {
                        Succeeded = true,
                        Errors = new() { "User added to role successfully" }
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding user {UserId} to role {RoleId}", UserId, RoleId);
                    return new OperationResultDto()
                    {
                        Succeeded = false,
                        Errors = new() { "Error adding user to role" }
                    };
                }
            }

            public async Task<OperationResultDto> RemoveUserFromRoleAsync(Guid RoleId, Guid UserId)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(RoleId.ToString());
                    if (role == null)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "Role not found" }
                        };
                    var user = await _userManager.FindByIdAsync(UserId.ToString());
                    if (user == null)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "User not found" }
                        };

                    // بررسی اینکه آیا کاربر این نقش را دارد
                    if (!await _userManager.IsInRoleAsync(user, role.Name))
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = new() { "User does not have this role" }
                        };

                    var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                        return new OperationResultDto()
                        {
                            Succeeded = false,
                            Errors = result.Errors.Select(e => e.Description).ToList()
                        };

                    _logger.LogInformation("User {UserId} removed from role {RoleName}", UserId, role.Name);
                    return new OperationResultDto()
                    {
                        Succeeded = true,
                        Message = "User removed from role successfully"
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error removing user {UserId} from role {RoleId}", UserId, RoleId);
                    return new OperationResultDto()
                    {
                        Succeeded = false,
                        Errors = new() { "Error removing user from role" }
                    };
                }
            }

            private bool IsSystemRole(string roleName)
            {
                var systemRoles = new[] { "SuperAdmin", "Admin", "User" };
                return systemRoles.Contains(roleName);
            }

            public async Task<bool> RoleExistByName(string name)
            {
                return await _roleManager.Roles.Where(x => x.Name == name)
                   .AnyAsync();
            }
        }
}
