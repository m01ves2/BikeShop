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
            return await _http.GetFromJsonAsync<List<ProductListItemModel>>(url) ?? [];
        }

        public async Task<List<ProductListItemModel>> GetProductsByCategoryAsync(int categoryId)
        {
            var url = $"api/categories/{categoryId}/products";
            return await _http.GetFromJsonAsync<List<ProductListItemModel>>(url) ?? [];
        }

        public async Task<ProductDetailsModel?> GetProductDetailsAsync(int id)
        {
            var url = $"api/products/{id}";
            return await _http.GetFromJsonAsync<ProductDetailsModel>(url);
        }
    }
}
