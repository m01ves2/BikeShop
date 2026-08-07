using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Domain.Entities;
using BikeShop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Persistence
{
    public class BikeShopDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IUnitOfWork
    {
        public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BikeShopDbContext).Assembly); //Configuration вместо OnModelCreating,
                                                                                              //то есть Entity -- Configuration вместо modelBuilder.Entity<Category>().HasKey(...)
                                                                                               //Это строка через Reflection находит все классы,
                                                                                              //реализующие IEntityTypeConfiguration<T>
                                                                                              
            //То есть происходит примерно следующее (неявно):
            //foreach (var configuration in allConfigurations) {
            //    modelBuilder.ApplyConfiguration(configuration);
            //}

        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await base.SaveChangesAsync(cancellationToken);
        }
    }
}
