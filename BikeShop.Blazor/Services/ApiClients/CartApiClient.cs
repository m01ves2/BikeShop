using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class CartApiClient : BaseApiClient
    {
        public CartApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) : base(factory, authStateProvider)
        {
        }

        public async Task<(CartModel? Cart, ApiErrorResponseModel? Error)> GetCartAsync()
        {
            AddAuthorizationHeader();

            //простой подход
            //var url = $"api/cart";
            //return await _http.GetFromJsonAsync<CartModel>(url);

            //с обработкой ошибок
            var url = $"api/cart";
            var response = await _http.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var cart = await response.Content.ReadFromJsonAsync<CartModel>();
                return (cart, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

        public async Task<ApiErrorResponseModel?> AddItemAsync(int productId)
        {
            AddAuthorizationHeader();

            var url = $"api/cart/{productId}";
            var response = await _http.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> RemoveItemAsync(int productId)
        {
            AddAuthorizationHeader();

            var url = $"api/cart/{productId}";
            var response = await _http.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> ClearCartAsync()
        {
            AddAuthorizationHeader();

            var url = "api/cart";
            var response = await _http.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> IncreaseItemQuantityAsync(int productId, int amount)
        {
            AddAuthorizationHeader();

            var url = $"api/cart/{productId}/increase?amount={amount}";
            var response = await _http.PatchAsync(url, null);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> DecreaseItemQuantityAsync(int productId, int amount)
        {
            AddAuthorizationHeader();

            var url = $"api/cart/{productId}/decrease?amount={amount}";
            var response = await _http.PatchAsync(url, null);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<(SynchronizeCartResultModel?, ApiErrorResponseModel?)> SynchronizeCartAsync(CartModel localCart)
        {
            AddAuthorizationHeader();

            var url = "api/cart/synchronize";
            var response = await _http.PostAsJsonAsync(url, localCart);
            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<SynchronizeCartResultModel>();
                return (result, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

    }
}