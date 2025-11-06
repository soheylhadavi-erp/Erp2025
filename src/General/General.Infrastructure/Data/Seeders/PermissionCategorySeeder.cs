using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Auth.Permissions
{
    public class PermissionCategorySeeder
    {
        public static async Task SeedPermissionCategoriesAsync(AppDbContext context)
        {
            if (!await context.PermissionCategories.AnyAsync())
            {
                var categories = new[]
                {
                new PermissionCategory {
                    Id = Guid.Parse("1cd50a7e-e634-40d3-aba2-35256e4fdc75"),
                    Name = "UserManagement",
                    Description = "مدیریت کاربران سیستم",
                    DisplayOrder = 1
                },
                new PermissionCategory {
                    Id = Guid.Parse("3e986d5e-a38f-46b9-9ea6-8c715c00ca0b"),
                    Name = "RoleManagement",
                    Description = "مدیریت نقش‌ها و دسترسی‌ها",
                    DisplayOrder = 2
                },
                new PermissionCategory {
                    Id = Guid.Parse("1530749c-fffd-4037-bb0a-e64223d83dea"),
                    Name = "ProductManagement",
                    Description = "مدیریت محصولات و خدمات",
                    DisplayOrder = 3
                },
                new PermissionCategory {
                    Id = Guid.Parse("587ac77c-0117-42c7-b070-6a8e3873f021"),
                    Name = "OrderManagement",
                    Description = "مدیریت سفارشات و تراکنش‌ها",
                    DisplayOrder = 4
                },
                new PermissionCategory {
                    Id = Guid.Parse("e0e0b847-0fa4-49b0-aa12-9582cabc4c1f"),
                    Name = "SystemManagement",
                    Description = "تنظیمات و مدیریت سیستم",
                    DisplayOrder = 5
                },
                new PermissionCategory {
                    Id = Guid.Parse("fe5addbc-90b0-47db-bfb1-fabaf48b521b"),
                    Name = "ReportManagement",
                    Description = "گزارشات و آمار سیستم",
                    DisplayOrder = 6
                }
        };

                await context.PermissionCategories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }
    }
}
