using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models.Catalog;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients.Catalog
{
    public class ProductApiClient : BaseApiClient
    {
        public ProductApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) 
            : base(factory, authStateProvider)
        {
        }

        public async Task<(List<ProductListItemModel>?, ApiErrorResponseModel?)> GetProductsAsync()
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync("api/products");

            if (response.IsSuccessStatusCode) {
                var products = await response.Content.ReadFromJsonAsync<List<ProductListItemModel>>();
                
                return (products, null);
            }

            var error = await ReadErrorAsync(response);

            return (null, error);
        }

        public async Task<(List<ProductListItemModel>?, ApiErrorResponseModel?)> GetProductsByCategoryAsync(int categoryId)
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync($"api/categories/{categoryId}/products");

            if (response.IsSuccessStatusCode) {
                var products = await response.Content.ReadFromJsonAsync<List<ProductListItemModel>>();
                
                return (products, null);
            }

            var error = await ReadErrorAsync(response);

            return (null, error);
        }

        public async Task<(ProductDetailsModel?, ApiErrorResponseModel?)> GetProductDetailsAsync(int id)
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync($"api/products/{id}");

            if (response.IsSuccessStatusCode) {
                var product = await response.Content.ReadFromJsonAsync<ProductDetailsModel>();

                return (product, null);
            }

            var error = await ReadErrorAsync(response);

            return (null, error);
        }
    }
}
