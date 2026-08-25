using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class AdminCategoriesApiClient : BaseApiClient
    {
        public AdminCategoriesApiClient(
            IHttpClientFactory factory,
            JwtAuthenticationStateProvider authStateProvider)
            : base(factory, authStateProvider)
        {
        }

        public async Task<ApiErrorResponseModel?> CreateAsync(CreateCategoryModel model)
        {
            AddAuthorizationHeader();

            var response = await _http.PostAsJsonAsync(
                "api/admin/categories",
                model);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> UpdateAsync(UpdateCategoryModel model)
        {
            AddAuthorizationHeader();

            var response = await _http.PutAsJsonAsync(
                $"api/admin/categories/{model.Id}",
                model);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> DeleteAsync(int id)
        {
            AddAuthorizationHeader();

            var response = await _http.DeleteAsync(
                $"api/admin/categories/{id}");

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<ApiErrorResponseModel>();
        }
    }
}
