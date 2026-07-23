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
            //return await _http.GetFromJsonAsync<List<CategoryModel>>("api/categories") ?? [];

            var url = "api/categories";

            Console.WriteLine(
                $"Request: {_http.BaseAddress}{url}");

            return await _http.GetFromJsonAsync<List<CategoryListItemModel>>(url)
                   ?? [];
        }
    }
}
