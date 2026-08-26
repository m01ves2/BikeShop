using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public abstract class BaseApiClient
    {
        protected readonly HttpClient _http;
        private readonly JwtAuthenticationStateProvider _authStateProvider;

        protected BaseApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider)
        {
            _http = factory.CreateClient("BikeShopApi");
            _authStateProvider = authStateProvider;
        }
        protected void AddAuthorizationHeader()
        {
            if (!string.IsNullOrWhiteSpace(_authStateProvider.Token)) {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authStateProvider.Token);
            }
            else {
                _http.DefaultRequestHeaders.Authorization = null;
            }
        }

        protected async Task<ApiErrorResponseModel> ReadErrorAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                await _authStateProvider.SignOutAsync();

                return new ApiErrorResponseModel("Your session has expired. Please log in again.");
            }

            try {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

                if (error is not null)
                    return error;
            }
            catch (JsonException) {
            }

            var content = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(content))
                return new ApiErrorResponseModel(content);

            return new ApiErrorResponseModel($"Request failed with status code {(int)response.StatusCode} ({response.StatusCode}).");
        }
    }
}
