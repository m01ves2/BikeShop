using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models.Admin.Products;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients.Admin.Products
{
    public class AdminProductsApiClient : BaseApiClient
    {
        public AdminProductsApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider)
            : base(factory, authStateProvider)
        {
        }

        public async Task<ApiErrorResponseModel?> CreateAsync(CreateProductModel model)
        {
            AddAuthorizationHeader();

            var response = await _http.PostAsJsonAsync("api/admin/products", model);

            if (response.IsSuccessStatusCode)
                return null;

            return await ReadErrorAsync(response);
        }

        public async Task<ApiErrorResponseModel?> UpdateAsync(UpdateProductModel model)
        {
            AddAuthorizationHeader();

            var response = await _http.PutAsJsonAsync($"api/admin/products/{model.Id}", model);

            if (response.IsSuccessStatusCode)
                return null;

            return await ReadErrorAsync(response);
        }

        public async Task<ApiErrorResponseModel?> DeleteAsync(int id)
        {
            AddAuthorizationHeader();

            var response = await _http.DeleteAsync($"api/admin/products/{id}");

            if (response.IsSuccessStatusCode)
                return null;

            return await ReadErrorAsync(response);
        }
    }
}
