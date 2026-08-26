using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models.Catalog;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients.Catalog
{
    public class CategoryApiClient : BaseApiClient
    {
        public CategoryApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) 
            : base(factory, authStateProvider)
        {
        }

        public async Task<(List<CategoryListItemModel>?, ApiErrorResponseModel?)> GetCategoriesAsync()
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync("api/categories");

            if (response.IsSuccessStatusCode) {
                var categories = await response.Content.ReadFromJsonAsync<List<CategoryListItemModel>>();
                
                return (categories, null);
            }

            var error = await ReadErrorAsync(response);

            return (null, error);
        }

        public async Task<(CategoryListItemModel?, ApiErrorResponseModel?)> GetCategoryByIdAsync(int categoryId)
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync($"api/categories/{categoryId}");

            if (response.IsSuccessStatusCode) {
                var category = await response.Content.ReadFromJsonAsync<CategoryListItemModel>();
                
                return (category, null);
            }

            var error = await ReadErrorAsync(response);

            return (null, error);
        }
    }
}
