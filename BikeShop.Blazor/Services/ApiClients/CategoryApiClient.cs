using System.Net.Http.Headers;
using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class CategoryApiClient : BaseApiClient
    {
        public CategoryApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) : base(factory, authStateProvider)
        {
        }

        public async Task<List<CategoryListItemModel>> GetCategoriesAsync()
        {
            AddAuthorizationHeader();

            var url = "api/categories";
            return await _http.GetFromJsonAsync<List<CategoryListItemModel>>(url) ?? [];
        }

        public async Task<CategoryListItemModel?> GetCategoryByIdAsync(int categoryId)
        {
            AddAuthorizationHeader();

            var url = $"api/categories/{categoryId}";
            return await _http.GetFromJsonAsync<CategoryListItemModel>(url);
        }
    }
}
