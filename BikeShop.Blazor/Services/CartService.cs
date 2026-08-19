using System.Text.Json;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.ApiClients;
using BikeShop.Blazor.Services.Models;
using Microsoft.JSInterop;

namespace BikeShop.Blazor.Services
{
    public class CartService : ICartService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly CartApiClient _cartApiClient;

        public event EventHandler? CartChanged;
        //public event EventHandler<IReadOnlyList<string>>? SynchronizeCartCompleted;

        public CartService(IJSRuntime jsRuntime, CartApiClient cartApiClient)
        {
            _jsRuntime = jsRuntime;
            _cartApiClient = cartApiClient;
        }

        public async Task<CartModel> GetLocalCartAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string?>("cartStorage.get");

            if (string.IsNullOrEmpty(json)) {
                return new CartModel(new List<CartItemModel>());
            }

            return JsonSerializer.Deserialize<CartModel>(json) ?? new CartModel(new List<CartItemModel>());
        }
        private async Task SaveLocalCartAsync(CartModel cart)
        {
            var json = JsonSerializer.Serialize(cart);

            await _jsRuntime.InvokeVoidAsync("cartStorage.set", json);
        }


        public async Task<(CartModel?, ApiErrorResponseModel?)> GetServerCartAsync()
        {
            return await _cartApiClient.GetCartAsync();
        }
        public async Task<ApiErrorResponseModel?> AddItemAsync(CartItemModel item)
        {
            var error = await _cartApiClient.AddItemAsync(item.Product.Id);

            if (error != null)
                return error;

            return await RefreshLocalCartAsync();
        }
        public async Task<ApiErrorResponseModel?> RemoveItemAsync(int productId)
        {
            var error = await _cartApiClient.RemoveItemAsync(productId);
            if (error != null) {
                return error;
            }

            return await RefreshLocalCartAsync();
        }
        public async Task<ApiErrorResponseModel?> IncreaseItemQuantityAsync(int productId, int amount)
        {
            var error = await _cartApiClient.IncreaseItemQuantityAsync(productId, amount);

            if (error != null)
                return error;

            return await RefreshLocalCartAsync();
        }
        public async Task<ApiErrorResponseModel?> DecreaseItemQuantityAsync(int productId, int amount)
        {
            var error = await _cartApiClient.DecreaseItemQuantityAsync(productId, amount);

            if (error != null)
                return error;

            return await RefreshLocalCartAsync();
        }
        public async Task<ApiErrorResponseModel?> ClearAsync()
        {
            var error = await _cartApiClient.ClearCartAsync();

            if (error != null)
                return error;

            return await RefreshLocalCartAsync();
        }


        public async Task<ApiErrorResponseModel?> RefreshLocalCartAsync()
        {
            var (serverCart, error) = await GetServerCartAsync();

            if (error != null)
                return error;

            await SaveLocalCartAsync(serverCart!);

            CartChanged?.Invoke(this, EventArgs.Empty);

            return null;
        }
        public async Task<(SynchronizeCartResultModel?, ApiErrorResponseModel?)> SynchronizeCartAsync()
        {
            var localCart = await GetLocalCartAsync();
            var (resultCart, error) = await _cartApiClient.SynchronizeCartAsync(localCart);

            if (error != null) {
                return (null, error);
            }

            var lcart = new CartModel(new List<CartItemModel>() );

            await SaveLocalCartAsync(resultCart!.Cart);
            CartChanged?.Invoke(this, EventArgs.Empty);

            return (resultCart, null);
        }
    }
}
