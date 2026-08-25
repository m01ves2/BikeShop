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

        public async Task<(List<OrderListItemModel>? Orders, ApiErrorResponseModel? Error)> GetOrdersAsync()
        {
            AddAuthorizationHeader();

            var response = await _http.GetAsync("api/orders");

            if (response.IsSuccessStatusCode) {
                var orders = await response.Content.ReadFromJsonAsync<List<OrderListItemModel>>();
                return (orders, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
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

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
            return (null, error);
        }

        public async Task<ApiErrorResponseModel?> CancelOrderAsync(int id)
        {
            AddAuthorizationHeader();

            var response = await _http.PutAsync($"api/orders/{id}/cancel", null);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> UpdateOrderAsync(UpdateOrderModel model)
        {
            AddAuthorizationHeader();

            var response = await _http.PutAsJsonAsync($"api/orders/{model.OrderId}", model);

            if (response.IsSuccessStatusCode)
                return null;

            //return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"STATUS: {response.StatusCode}");
            Console.WriteLine($"CONTENT: {content}");

            return new ApiErrorResponseModel(content);
        }
    }
}
