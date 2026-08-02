using System.Net.Http;
using BikeShop.Blazor.Models;

namespace BikeShop.Blazor.Services
{
    public class AuthApiClient
    {
        private readonly HttpClient _http;

        public AuthApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode;
        }
    }
}
