using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Categories.CreateCategory;
using BikeShop.Application.Categories.DeleteCategory;
using BikeShop.Application.Categories.GetCategories;
using BikeShop.Application.Categories.UpdateCategory;
using BikeShop.Application.Products.CreateProduct;
using BikeShop.Application.Products.DeleteProduct;
using BikeShop.Application.Products.GetProducts;
using BikeShop.Application.Products.UpdateProduct;
using Microsoft.Extensions.DependencyInjection;
using BikeShop.Application.Products.GetProductDetails;

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

            services.AddScoped<IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryListItemDto>>>, GetCategoriesQueryHandler>();
            services.AddScoped<ICommandHandler<CreateCategoryCommand, Result>, CreateCategoryCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateCategoryCommand, Result>, UpdateCategoryCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteCategoryCommand, Result>, DeleteCategoryCommandHandler>();

            services.AddScoped<IQueryHandler<GetProductsQuery, Result<IReadOnlyList<ProductListItemDto>>>, GetProductsQueryHandler>();
            services.AddScoped<IQueryHandler<GetProductDetailsQuery, Result<ProductDetailsDto>>, GetProductDetailsQueryHandler>();
            services.AddScoped<ICommandHandler<CreateProductCommand, Result>, CreateProductCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateProductCommand, Result>, UpdateProductCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteProductCommand, Result>, DeleteProductCommandHandler>();
            return services;
        }
    }
}
