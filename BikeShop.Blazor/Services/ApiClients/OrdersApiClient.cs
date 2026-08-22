using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class OrdersApiClient : BaseApiClient
    {
        public OrdersApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider)
            : base(factory, authStateProvider)
        {
        }

        public async Task<(CreateOrderResultModel? Result, ApiErrorResponseModel? Error)> CreateOrderAsync(CreateOrderModel model)
        {
            AddAuthorizationHeader();

            var response = await _http.PostAsJsonAsync("api/orders", model);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<CreateOrderResultModel>();

                return (result, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }
    }
}
