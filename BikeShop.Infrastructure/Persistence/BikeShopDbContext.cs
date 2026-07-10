using BikeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BikeShop.Infrastructure.Persistence
{
    public class BikeShopDbContext : DbContext
    {
        public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BikeShopDbContext).Assembly); //Configuration вместо OnModelCreating,
                                                                                              //то есть Entity -- Configuration вместо modelBuilder.Entity<Category>().HasKey(...)
        }
    }
}
