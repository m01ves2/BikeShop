using BikeShop.Blazor.Services;

namespace BikeShop.Blazor
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBlazorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CategoryApiClient>();
            services.AddScoped<ProductApiClient>();

            services.AddScoped<HttpClient>(sp =>
            {
                return new HttpClient
                {
                    BaseAddress = new Uri(
                          "https://localhost:7263/")
                };
            });


            //services.AddHttpClient<CategoryApiClient>(client =>
            //{
            //    client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!); //appsettings.json
            //});



            return services;
        }
    }
}
