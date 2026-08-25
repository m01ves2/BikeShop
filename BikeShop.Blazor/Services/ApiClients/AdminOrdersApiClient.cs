using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class AdminOrdersApiClient : BaseApiClient
    {
        public AdminOrdersApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider)
            : base(factory, authStateProvider)
        {
        }

        public async Task<(AdminOrderDetailsModel? Order, ApiErrorResponseModel? Error)> GetOrderByIdAsync(int id)
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync($"api/admin/orders/{id}");

            if (response.IsSuccessStatusCode) {
                var order = await response.Content.ReadFromJsonAsync<AdminOrderDetailsModel>();
                return (order, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
            return (null, error);
        }

        public async Task<ApiErrorResponseModel?> ChangeStatusAsync(ChangeOrderStatusModel model)
        {
            AddAuthorizationHeader();

            var response = await _http.PutAsJsonAsync($"api/admin/orders/{model.OrderId}/status", model);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }
    }
}
