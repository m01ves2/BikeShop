using BikeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(
            BikeShopDbContext context)
        {
            if (await context.Categories.AnyAsync())
                return;

            var categories = new[]
            {
            new Category("Mountain Bikes"),
            new Category("Road Bikes"),
            new Category("City Bikes")
        };

            await context.Categories.AddRangeAsync(categories);

            await context.SaveChangesAsync();
        }
    }
}
