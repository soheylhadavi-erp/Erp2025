
using General.Infrastructure.Security.Services;
using General.Infrastructure.Security.Services.Infrastructure.Identity.Services;
using global::General.Infrastructure.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace General.Infrastructure.Data.Seeders
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var userService = services.GetRequiredService<IdentityService>();
            var roleService = services.GetRequiredService<RoleService>();
            var logger = services.GetRequiredService<ILogger<UserSeeder>>();

            try
            {
                // ۱. ایجاد رول ادمین اگر وجود ندارد
                var adminRoleName = "Admin";
                var adminRoleExists = await roleService..AnyAsync(r => r.Name == adminRoleName);
                if (!adminRoleExists)
                {
                    var adminRole = new ApplicationRole()
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        Description = "Admin Role With Full Permissions"
                    };
                    await roleManager.CreateAsync(adminRole);
                    logger.LogInformation("Admin role created");
                }

                // ۲. ایجاد کاربر ادمین اگر وجود ندارد
                var adminEmail = "admin@example.com";
                var adminPassword = "Admin123!";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        FullName = "Admin",
                        CreateDateTime = DateTime.UtcNow,

                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);

                    if (result.Succeeded)
                    {
                        // ۳. اختصاص رول به کاربر
                        await userManager.AddToRoleAsync(adminUser, adminRoleName);

                        logger.LogInformation("Admin user created successfully");
                        logger.LogInformation("Email: {Email}, Password: {Password}", adminEmail, "Admin123!");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}",
                            string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
                else
                {
                    logger.LogInformation("Admin user already exists");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding admin user");
                throw;
            }
        }
    }
}

