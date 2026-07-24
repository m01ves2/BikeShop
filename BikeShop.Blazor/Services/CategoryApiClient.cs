using BikeShop.Blazor.Models;

namespace BikeShop.Blazor.Services
{
    public class CategoryApiClient
    {
        private readonly HttpClient _http;

        public CategoryApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoryListItemModel>> GetCategoriesAsync()
        {
            var url = "api/categories";
            return await _http.GetFromJsonAsync<List<CategoryListItemModel>>(url) ?? [];
        }

        public async Task<CategoryListItemModel?> GetCategoryByIdAsync(int categoryId)
        {
            var url = $"api/categories/{categoryId}";
            return await _http.GetFromJsonAsync<CategoryListItemModel>(url);
        }
    }
}
