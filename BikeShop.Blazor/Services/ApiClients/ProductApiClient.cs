using System.Net.Http.Headers;
using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class ProductApiClient : BaseApiClient
    {
        public ProductApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) : base(factory, authStateProvider)
        {
        }

        public async Task<List<ProductListItemModel>> GetProductsAsync()
        {
            AddAuthorizationHeader();

            var url = "api/products";
            return await _http.GetFromJsonAsync<List<ProductListItemModel>>(url) ?? [];
        }

        public async Task<List<ProductListItemModel>> GetProductsByCategoryAsync(int categoryId)
        {
            AddAuthorizationHeader();

            var url = $"api/categories/{categoryId}/products";
            return await _http.GetFromJsonAsync<List<ProductListItemModel>>(url) ?? [];
        }

        public async Task<ProductDetailsModel?> GetProductDetailsAsync(int id)
        {
            AddAuthorizationHeader();

            var url = $"api/products/{id}";
            return await _http.GetFromJsonAsync<ProductDetailsModel>(url);
        }
    }
}
