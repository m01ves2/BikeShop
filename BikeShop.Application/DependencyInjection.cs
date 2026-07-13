using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Categories.CreateCategory;
using BikeShop.Application.Categories.DeleteCategory;
using BikeShop.Application.Categories.GetCategories;
using BikeShop.Application.Categories.UpdateCategory;
using BikeShop.Application.Common.Models;
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
            services.AddScoped<IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryDto>>>, GetCategoriesQueryHandler>();
            services.AddScoped<ICommandHandler<CreateCategoryCommand, Result>, CreateCategoryCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateCategoryCommand, Result>, UpdateCategoryCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteCategoryCommand, Result>, DeleteCategoryCommandHandler>();
            return services;
        }
    }
}
