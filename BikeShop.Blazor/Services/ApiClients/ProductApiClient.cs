using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class ProductApiClient : BaseApiClient
    {
        public ProductApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) : base(factory, authStateProvider)
        {
        }

        public async Task<(List<ProductListItemModel>?, ApiErrorResponseModel?)> GetProductsAsync()
        {
            AddAuthorizationHeader();

            var url = "api/products";
            var response = await _http.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var products = await response.Content.ReadFromJsonAsync<List<ProductListItemModel>>();
                return (products, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

        public async Task<(List<ProductListItemModel>?, ApiErrorResponseModel?)> GetProductsByCategoryAsync(int categoryId)
        {
            AddAuthorizationHeader();

            var url = $"api/categories/{categoryId}/products";
            var response = await _http.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var products = await response.Content.ReadFromJsonAsync<List<ProductListItemModel>>();
                return (products, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

        public async Task<(ProductDetailsModel?, ApiErrorResponseModel?)> GetProductDetailsAsync(int id)
        {
            AddAuthorizationHeader();

            var url = $"api/products/{id}";
            var response = await _http.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var product = await response.Content.ReadFromJsonAsync<ProductDetailsModel>();
                return (product, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }
    }
}
