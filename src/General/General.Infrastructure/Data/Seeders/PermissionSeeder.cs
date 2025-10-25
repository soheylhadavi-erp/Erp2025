using General.Infrastructure.Data;
using General.Infrastructure.Security.Entities;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Data.Seeders
{
    public class PermissionSeeder
    {
        public static async Task SeedPermissionsAsync(AppDbContext context)
        {
            if (!await context.SystemPermissions.AnyAsync())
            {
                var permissions = new[]
                {
                    // User Management (CategoryId = 1)
                    new SystemPermission { Name = "Users.View", Description = "مشاهده کاربران", CategoryId = Guid.Parse("1cd50a7e-e634-40d3-aba2-35256e4fdc75") },
                    new SystemPermission { Name = "Users.Create", Description = "ایجاد کاربر", CategoryId = Guid.Parse("1cd50a7e-e634-40d3-aba2-35256e4fdc75") },
                    new SystemPermission { Name = "Users.Edit", Description = "ویرایش کاربر", CategoryId = Guid.Parse("1cd50a7e-e634-40d3-aba2-35256e4fdc75") },
                    new SystemPermission { Name = "Users.Delete", Description = "حذف کاربر", CategoryId = Guid.Parse("1cd50a7e-e634-40d3-aba2-35256e4fdc75") },

                    // Role Management (CategoryId = 2)
                    new SystemPermission { Name = "Roles.View", Description = "مشاهده نقش‌ها", CategoryId = Guid.Parse("3e986d5e-a38f-46b9-9ea6-8c715c00ca0b") },
                    new SystemPermission { Name = "Roles.Create", Description = "ایجاد نقش", CategoryId = Guid.Parse("3e986d5e-a38f-46b9-9ea6-8c715c00ca0b") },
                    new SystemPermission { Name = "Roles.Edit", Description = "ویرایش نقش", CategoryId = Guid.Parse("3e986d5e-a38f-46b9-9ea6-8c715c00ca0b") },
                    new SystemPermission { Name = "Roles.Delete", Description = "حذف نقش", CategoryId = Guid.Parse("3e986d5e-a38f-46b9-9ea6-8c715c00ca0b") },

                    // Product Management (CategoryId = 3)
                    new SystemPermission { Name = "Products.View", Description = "مشاهده محصولات", CategoryId = Guid.Parse("1530749c-fffd-4037-bb0a-e64223d83dea") },
                    new SystemPermission { Name = "Products.Create", Description = "ایجاد محصول", CategoryId = Guid.Parse("1530749c-fffd-4037-bb0a-e64223d83dea") },
                    new SystemPermission { Name = "Products.Edit", Description = "ویرایش محصول", CategoryId = Guid.Parse("1530749c-fffd-4037-bb0a-e64223d83dea") },
                    new SystemPermission { Name = "Products.Delete", Description = "حذف محصول", CategoryId = Guid.Parse("1530749c-fffd-4037-bb0a-e64223d83dea") }
                };

                await context.SystemPermissions.AddRangeAsync(permissions);
                await context.SaveChangesAsync();
            }
        }
    }
}
