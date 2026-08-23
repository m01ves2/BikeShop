using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Authentication.DTOs;
using BikeShop.Application.Authentication.Login;
using BikeShop.Application.Authentication.Register;
using BikeShop.Application.Carts.AddItem;
using BikeShop.Application.Carts.ClearCart;
using BikeShop.Application.Carts.DecreaseItemQuantity;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Carts.GetCartByCustomerId;
using BikeShop.Application.Carts.IncreaseItemQuantity;
using BikeShop.Application.Carts.RemoveItem;
using BikeShop.Application.Carts.SynchronizeCart;
using BikeShop.Application.Categories.CreateCategory;
using BikeShop.Application.Categories.DeleteCategory;
using BikeShop.Application.Categories.DTOs;
using BikeShop.Application.Categories.GetCategories;
using BikeShop.Application.Categories.GetCategoryDetails;
using BikeShop.Application.Categories.UpdateCategory;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.CreateOrder;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Application.Orders.GetCustomerOrders;
using BikeShop.Application.Orders.GetOrderDetails;
using BikeShop.Application.Products.CreateProduct;
using BikeShop.Application.Products.DeleteProduct;
using BikeShop.Application.Products.DTOs;
using BikeShop.Application.Products.GetProductDetails;
using BikeShop.Application.Products.GetProducts;
using BikeShop.Application.Products.GetProductsByCategoryId;
using BikeShop.Application.Products.UpdateProduct;
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

            services.AddScoped<IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryListItemDto>>>, GetCategoriesQueryHandler>();
            services.AddScoped<IQueryHandler<GetCategoryDetailsQuery, Result<CategoryDetailsDto>>, GetCategoryDetailsQueryHandler>();

            services.AddScoped<ICommandHandler<CreateCategoryCommand, Result>, CreateCategoryCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateCategoryCommand, Result>, UpdateCategoryCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteCategoryCommand, Result>, DeleteCategoryCommandHandler>();



            services.AddScoped<IQueryHandler<GetProductsQuery, Result<IReadOnlyList<ProductListItemDto>>>, GetProductsQueryHandler>();
            services.AddScoped<IQueryHandler<GetProductsByCategoryIdQuery, Result<IReadOnlyList<ProductListItemDto>>>, GetProductsByCategoryIdQueryHandler>();
            services.AddScoped<IQueryHandler<GetProductDetailsQuery, Result<ProductDetailsDto>>, GetProductDetailsQueryHandler>();
            
            services.AddScoped<ICommandHandler<CreateProductCommand, Result>, CreateProductCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateProductCommand, Result>, UpdateProductCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteProductCommand, Result>, DeleteProductCommandHandler>();

            services.AddScoped<ICommandHandler<RegisterUserCommand, Result>, RegisterUserCommandHandler>();
            services.AddScoped<ICommandHandler<LoginUserCommand, Result<LoginDto>>, LoginUserCommandHandler>();

            services.AddScoped<IQueryHandler<GetCartQuery, Result<CartDto>>, GetCartQueryHandler>();
            services.AddScoped<ICommandHandler<AddItemCommand, Result>, AddItemCommandHandler>();
            services.AddScoped<ICommandHandler<ClearCartCommand, Result>, ClearCartCommandHandler>();
            services.AddScoped<ICommandHandler<RemoveItemCommand, Result>, RemoveItemCommandHandler>();
            services.AddScoped<ICommandHandler<IncreaseItemQuantityCommand, Result>, IncreaseItemQuantityCommandHandler>();
            services.AddScoped<ICommandHandler<DecreaseItemQuantityCommand, Result>, DecreaseItemQuantityCommandHandler>();
            services.AddScoped<ICommandHandler<SynchronizeCartCommand, Result<SynchronizeCartResultDto>>, SynchronizeCartCommandHandler>();

            services.AddScoped<ICommandHandler<CreateOrderCommand, Result<CreateOrderResultDto>>, CreateOrderCommandHandler>();
            services.AddScoped<IQueryHandler<GetCustomerOrdersQuery, Result<IReadOnlyList<OrderListItemDto>>>, GetCustomerOrdersQueryHandler>();
            services.AddScoped<IQueryHandler<GetOrderDetailsQuery, Result<OrderDetailsDto>>, GetOrderDetailsQueryHandler>();

            return services;
        }
    }
}
