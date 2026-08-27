using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models.Auth;
using BikeShop.Blazor.Services.Models;
using System.Net;

namespace BikeShop.Blazor.Services.ApiClients.Auth
{
    public class AuthApiClient : BaseApiClient
    {
        public AuthApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider)
            : base(factory, authStateProvider)
        {
        }

        public async Task<ApiErrorResponseModel?> RegisterAsync(RegisterModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);

            if (response.IsSuccessStatusCode) {
                return null;
            }

            return await ReadErrorAsync(response);
        }

        public async Task<(LoginResponseModel? Login, ApiErrorResponseModel? Error)> LoginAsync(LoginModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", model);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseModel>();

                return (result, null);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                return (null, new ApiErrorResponseModel("Invalid email or password."));
            }

            var error = await ReadErrorAsync(response);

            return (null, error);
        }

        public async Task<(CurrentUserModel?, ApiErrorResponseModel?)> GetCurrentUserAsync()
        {
            AddAuthorizationHeader();

            //var result = await _http.GetFromJsonAsync<CurrentUserModel>("api/auth/me");
            var response = await _http.GetAsync("api/auth/me");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<CurrentUserModel>();

                return (result, null);
            }

            var error = await ReadErrorAsync(response);

            return (null, error);
        }
    }
}
