using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class AuthApiClient : BaseApiClient
    {
        public AuthApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) : base(factory, authStateProvider)
        {
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

        public async Task<CurrentUserModel> GetCurrentUserAsync()
        {
            AddAuthorizationHeader();

            var result = await _http.GetFromJsonAsync<CurrentUserModel>("api/auth/me");

            return result;
        }

    }
}
