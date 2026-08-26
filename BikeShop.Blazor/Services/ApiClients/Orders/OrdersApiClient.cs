using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models.Orders;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients.Orders
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

            var error = await ReadErrorAsync(response);

            return (null, error);
        }

        public async Task<(List<OrderListItemModel>? Orders, ApiErrorResponseModel? Error)> GetOrdersAsync()
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync("api/orders");

            if (response.IsSuccessStatusCode) {
                var orders = await response.Content.ReadFromJsonAsync<List<OrderListItemModel>>();
                
                return (orders, null);
            }

            var error = await ReadErrorAsync(response);
            return (null, error);
        }

        public async Task<(OrderDetailsModel? Order, ApiErrorResponseModel? Error)> GetOrderByIdAsync(int id)
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync($"api/orders/{id}");

            if (response.IsSuccessStatusCode) {
                var order = await response.Content.ReadFromJsonAsync<OrderDetailsModel>();
                
                return (order, null);
            }

            var error = await ReadErrorAsync(response);
            
            return (null, error);
        }

        public async Task<ApiErrorResponseModel?> CancelOrderAsync(int id)
        {
            AddAuthorizationHeader();

            var response = await _http.PutAsync($"api/orders/{id}/cancel", null);

            if (response.IsSuccessStatusCode)
                return null;

            return await ReadErrorAsync(response);
        }

        public async Task<ApiErrorResponseModel?> UpdateOrderAsync(UpdateOrderModel model)
        {
            AddAuthorizationHeader();

            var response = await _http.PutAsJsonAsync($"api/orders/{model.OrderId}", model);

            if (response.IsSuccessStatusCode)
                return null;

            return await ReadErrorAsync(response);
        }
    }
}
