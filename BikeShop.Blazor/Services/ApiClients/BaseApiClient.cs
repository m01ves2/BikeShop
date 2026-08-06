using System.Net.Http.Headers;
using BikeShop.Blazor.Identity;
using static System.Net.WebRequestMethods;

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
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        _authStateProvider.Token);
            }
            else {
                _http.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}
