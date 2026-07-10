using Microsoft.Extensions.DependencyInjection;

namespace BikeShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // MediatR
            // FluentValidation
            // AutoMapper/Mapster
            // Behaviors

            return services;
        }
    }
}
