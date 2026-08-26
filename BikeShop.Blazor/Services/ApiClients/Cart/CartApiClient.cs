using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models.Cart;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients.Cart
{
    public class CartApiClient : BaseApiClient
    {
        public CartApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) 
            : base(factory, authStateProvider)
        {
        }

        public async Task<(CartModel? Cart, ApiErrorResponseModel? Error)> GetCartAsync()
        {
            AddAuthorizationHeader();

            //простой подход
            //return await _http.GetFromJsonAsync<CartModel>($"api/cart");

            //с обработкой ошибок
            var response = await _http.GetAsync($"api/cart");

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

            var response = await _http.PostAsync($"api/cart/{productId}", null); //просто сделать HTTP POST с указанным HttpContent.

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> RemoveItemAsync(int productId)
        {
            AddAuthorizationHeader();

            var response = await _http.DeleteAsync($"api/cart/{productId}");

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> ClearCartAsync()
        {
            AddAuthorizationHeader();

            var response = await _http.DeleteAsync("api/cart");

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> IncreaseItemQuantityAsync(int productId, int amount)
        {
            AddAuthorizationHeader();

            var response = await _http.PatchAsync($"api/cart/{productId}/increase?amount={amount}", null);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<ApiErrorResponseModel?> DecreaseItemQuantityAsync(int productId, int amount)
        {
            AddAuthorizationHeader();

            var response = await _http.PatchAsync($"api/cart/{productId}/decrease?amount={amount}", null);

            if (response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        }

        public async Task<(SynchronizeCartResultModel?, ApiErrorResponseModel?)> SynchronizeCartAsync(CartModel localCart)
        {
            AddAuthorizationHeader();

            var response = await _http.PostAsJsonAsync("api/cart/synchronize", localCart); //это специализированный вариант PostAsync,
                                                                                           //который сам сериализует объект в JSON и выставляет нужный Content-Type.
            
            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<SynchronizeCartResultModel>();
                return (result, null);
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();

            return (null, error);
        }

    }
}