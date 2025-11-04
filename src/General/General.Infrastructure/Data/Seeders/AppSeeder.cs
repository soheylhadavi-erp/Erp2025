using General.Infrastructure.Data;
using General.Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public static class AppSeeder
{
    public static async Task SeedAsync(AppDbContext context,IServiceProvider serviceProvider,ILoggerFactory loggerFactory)
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