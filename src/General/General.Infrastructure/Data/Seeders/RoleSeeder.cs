using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace General.Infrastructure.Auth.Roles
{
    public class RoleSeeder
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public RoleSeeder(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("RoleSeeder");
        }

        public async Task SeedRolesAsync()
        {
            try
            {
                await SeedDefaultRoles();
                await _context.SaveChangesAsync();
                _logger.LogInformation("Role seeding completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding roles.");
                throw;
            }
        }

        private async Task SeedDefaultRoles()
        {
            var defaultRoles = new List<ApplicationRole>
        {
            new ApplicationRole
            {
                Name = "SuperAdmin",
                Description = "دسترسی کامل به تمام بخش‌های سیستم",
                //IsActive = true,
                //CreatedAt = DateTime.UtcNow
            },
            new ApplicationRole
            {
                NormalizedName="ADMIN",
                Name = "Admin",
                Description = "دسترسی مدیریتی به سیستم",
                //IsActive = true,
                //CreatedAt = DateTime.UtcNow
            },
            new ApplicationRole
            {
                Name = "User",
                Description = "دسترسی معمولی کاربران",
                //IsActive = true,
                //CreatedAt = DateTime.UtcNow
            },
            new ApplicationRole
            {
                Name = "Manager",
                Description = "دسترسی مدیریت بخش",
                //IsActive = true,
                //CreatedAt = DateTime.UtcNow
            },
            new ApplicationRole
            {
                Name = "Editor",
                Description = "دسترسی ویرایش محتوا",
                //IsActive = true,
                //CreatedAt = DateTime.UtcNow
            },
            new ApplicationRole
            {
                Name = "Viewer",
                Description = "دسترسی فقط مشاهده",
                //IsActive = true,
                //CreatedAt = DateTime.UtcNow
            }
        };

            foreach (var role in defaultRoles)
            {
                // بررسی وجود نقش
                var existingRole = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Name == role.Name);

                if (existingRole == null)
                {
                    _context.Roles.Add(role);
                    _logger.LogInformation("Role {RoleName} added.", role.Name);
                }
                else
                {
                    // آپدیت اطلاعات اگر تغییر کرده
                    existingRole.Description = role.Description;
                    //existingRole.IsActive = role.IsActive;
                    _logger.LogInformation("Role {RoleName} already exists.", role.Name);
                }
            }
        }

        // متد برای ریست کردن نقش‌ها (اختیاری)
        public async Task ResetToDefaultAsync()
        {
            try
            {
                // حذف تمام نقش‌ها (مراقب باشید در پروduction استفاده نکنید)
                var allRoles = await _context.Roles.ToListAsync();
                _context.Roles.RemoveRange(allRoles);

                await SeedRolesAsync();
                _logger.LogInformation("Roles reset to default completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while resetting roles.");
                throw;
            }
        }
    }
}
