using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Services;
using BikeShop.Blazor.Services.ApiClients;
using Microsoft.AspNetCore.Components.Authorization;

namespace BikeShop.Blazor
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBlazorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CurrencyFormatter>();

            //services.AddScoped<HttpClient>(sp =>
            //{
            //    return new HttpClient
            //    {
            //        BaseAddress = new Uri("https://localhost:7263/")
            //    };
            //});

            services.AddHttpClient("BikeShopApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7263/");
            });

            //services.AddHttpClient("BikeShopApi", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7263/");
            //})
            //.AddHttpMessageHandler<JwtAuthorizationMessageHandler>();


            services.AddScoped<CategoryApiClient>();
            services.AddScoped<ProductApiClient>();

            //services.AddHttpClient<CategoryApiClient>(client =>
            //{
            //    client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!); //appsettings.json
            //});

            services.AddScoped<BreadcrumbService>();

            services.AddScoped<ICartService, CartService>();

            services.AddScoped<AuthApiClient>();

            services.AddScoped<ITokenStorage, TokenStorage>();

            services.AddScoped<JwtAuthenticationStateProvider>();

            services.AddScoped<AuthenticationStateProvider>(sp =>
                sp.GetRequiredService<JwtAuthenticationStateProvider>());

            services.AddScoped<CartApiClient>();

            services.AddScoped<OrdersApiClient>();

            services.AddScoped<AdminOrdersApiClient>();

            services.AddScoped<AdminProductsApiClient>();

            services.AddScoped<AdminCategoriesApiClient>();

            return services;
        }
    }
}
