namespace General.Infrastructure.Auth.Users
{
    using General.Infrastructure.Auth.Roles;
    using General.Infrastructure.Security.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            var logger = services.GetRequiredService<ILogger<UserSeeder>>();

            try
            {
                var adminRoleName = "Admin";
                var adminEmail = "admin@example.com";

                ApplicationRole role = new ApplicationRole()
                {
                    Name = adminRoleName,
                    Description = "Admin Role with Full Permissions"

                };


                // ۱. ایجاد رول
                if (!await roleManager.RoleExistsAsync(adminRoleName))
                {
                    await roleManager.CreateAsync(role);
                    logger.LogInformation("✅ Admin role created");
                }

                // ۲. ایجاد کاربر
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
                        CreatorId = null,
                        IsDeleted = false
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin123!");

                    if (result.Succeeded)
                    {
                        // 🔥 مهم: کاربر رو دوباره load کن تا Id پر بشه
                        adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser != null && adminUser.Id != null)
                        {
                            await userManager.AddToRoleAsync(adminUser, adminRoleName);
                            logger.LogInformation("✅ Admin user created and role assigned");
                        }
                        else
                        {
                            logger.LogError("❌ User created but Id is null");
                        }
                    }
                }
                else
                {
                    // کاربر از قبل وجود داره
                    if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
                    {
                        await userManager.AddToRoleAsync(adminUser, adminRoleName);
                        logger.LogInformation("✅ Admin role assigned to existing user");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Error in seeder");
                throw;
            }
        }
    }
}





