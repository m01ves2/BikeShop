using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Infrastructure.Identity;
using BikeShop.Infrastructure.Persistence;
using BikeShop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<BikeShopDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork>(options => options.GetRequiredService<BikeShopDbContext>());

            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            // Email

            // DateTimeProvider

            //Identity
            services.AddScoped<IUserService, IdentityUserService>();

            //JWT for Identity login
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            return services;
        }
    }
}
