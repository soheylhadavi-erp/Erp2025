using General.Infrastructure.Auth.Permissions;
using General.Infrastructure.Auth.Roles;
using General.Infrastructure.Auth.Users;
using Microsoft.Extensions.Logging;
namespace General.Infrastructure
{
    public static class AppSeeder
    {
        public static async Task SeedAsync(AppDbContext context, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            //if (!await context.SystemPermissions.AnyAsync())
            //{
            await PermissionCategorySeeder.SeedPermissionCategoriesAsync(context);
            await PermissionSeeder.SeedPermissionsAsync(context);
            await new RoleSeeder(context, loggerFactory).SeedRolesAsync();
            await UserSeeder.SeedUsersAsync(serviceProvider);

            //}
        }
    }
}