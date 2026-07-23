using BikeShop.Blazor.Models;

namespace BikeShop.Blazor.Services
{
    public class ProductApiClient
    {
        private readonly HttpClient _http;

        public ProductApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductListItemModel>> GetProductsAsync()
        {
            var url = "api/products";

            Console.WriteLine($"Request: {_http.BaseAddress}{url}");

            return await _http.GetFromJsonAsync<List<ProductListItemModel>>(url) ?? [];
        }
    }
}
