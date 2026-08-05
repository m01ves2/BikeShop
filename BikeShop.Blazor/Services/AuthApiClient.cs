using System.Net.Http;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services
{
    public class AuthApiClient
    {
        private readonly HttpClient _http;

        public AuthApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiErrorResponseModel?> RegisterAsync(RegisterModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);

            if (response.IsSuccessStatusCode) {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<(LoginResponseModel? Login, ApiErrorResponseModel? Error)> LoginAsync(LoginModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", model);

            if (response.IsSuccessStatusCode) {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseModel>();

                return (loginResponse, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

    }
}
