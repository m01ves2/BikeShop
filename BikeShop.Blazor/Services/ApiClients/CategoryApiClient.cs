using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class CategoryApiClient : BaseApiClient
    {
        public CategoryApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) : base(factory, authStateProvider)
        {
        }

        public async Task<(List<CategoryListItemModel>?, ApiErrorResponseModel?)> GetCategoriesAsync()
        {
            AddAuthorizationHeader();

            var url = "api/categories";
            var response = await _http.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var categories = await response.Content.ReadFromJsonAsync<List<CategoryListItemModel>>();
                return (categories, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

        public async Task<(CategoryListItemModel?, ApiErrorResponseModel?)> GetCategoryByIdAsync(int categoryId)
        {
            AddAuthorizationHeader();

            var url = $"api/categories/{categoryId}";
            var response = await _http.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var category = await response.Content.ReadFromJsonAsync<CategoryListItemModel>();
                return (category, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }
    }
}
