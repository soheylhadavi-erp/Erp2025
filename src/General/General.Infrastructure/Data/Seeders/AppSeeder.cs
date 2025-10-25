// Infrastructure/Seeders/AppSeeder.cs
using General.Infrastructure.Data;
using General.Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;

public static class AppSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.SystemPermissions.AnyAsync())
        {
            await PermissionCategorySeeder.SeedPermissionCategoriesAsync(context);
            await PermissionSeeder.SeedPermissionsAsync(context);
        }
    }
}