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
            var url = "api/auth/register";
            var response = await _http.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode) {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<(LoginResponseModel? Login, ApiErrorResponseModel? Error)> LoginAsync(LoginModel model)
        {
            var url = "api/auth/login";
            var response = await _http.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseModel>();

                return (result, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

        public async Task<(CurrentUserModel?, ApiErrorResponseModel?)> GetCurrentUserAsync()
        {
            AddAuthorizationHeader();

            var url = "api/auth/me";
            //var result = await _http.GetFromJsonAsync<CurrentUserModel>(url);
            var response = await _http.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<CurrentUserModel>();

                return (result, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

    }
}
