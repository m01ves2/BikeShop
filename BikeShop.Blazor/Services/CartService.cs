using System.Text.Json;
using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models.Cart;
using BikeShop.Blazor.Services.ApiClients.Cart;
using BikeShop.Blazor.Services.Models;
using Microsoft.JSInterop;

namespace BikeShop.Blazor.Services
{
    public class CartService : ICartService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly CartApiClient _cartApiClient;
        private readonly JwtAuthenticationStateProvider _authStateProvider;

        private bool IsAuthenticated => !string.IsNullOrWhiteSpace(_authStateProvider.Token);

        public event EventHandler? CartChanged;

        public CartService(IJSRuntime jsRuntime, CartApiClient cartApiClient, JwtAuthenticationStateProvider authStateProvider)
        {
            _jsRuntime = jsRuntime;
            _cartApiClient = cartApiClient;
            _authStateProvider = authStateProvider;
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
            if (!IsAuthenticated)
                return (null, null);

            return await _cartApiClient.GetCartAsync();
        }
        public async Task<ApiErrorResponseModel?> AddItemAsync(CartItemModel item)
        {
            if (!IsAuthenticated) {
                var localCart = await GetLocalCartAsync();

                var existing = localCart.Items.FirstOrDefault(x => x.Product.Id == item.Product.Id);

                if (existing is not null)
                    existing.Quantity += item.Quantity;
                else
                    localCart.Items.Add(item);

                await SaveLocalCartAsync(localCart);

                CartChanged?.Invoke(this, EventArgs.Empty);

                return null;
            }

            var error = await _cartApiClient.AddItemAsync(item.Product.Id);

            if (error != null)
                return error;

            return await RefreshLocalCartAsync();
        }
        public async Task<ApiErrorResponseModel?> RemoveItemAsync(int productId)
        {
            if (!IsAuthenticated) {
                var localCart = await GetLocalCartAsync();

                localCart.Items.RemoveAll(x => x.Product.Id == productId);

                await SaveLocalCartAsync(localCart);

                CartChanged?.Invoke(this, EventArgs.Empty);

                return null;
            }

            var error = await _cartApiClient.RemoveItemAsync(productId);

            if (error is not null)
                return error;

            return await RefreshLocalCartAsync();
        }
        public async Task<ApiErrorResponseModel?> IncreaseItemQuantityAsync(int productId, int amount)
        {
            if (!IsAuthenticated) {
                var localCart = await GetLocalCartAsync();

                var item = localCart.Items.FirstOrDefault(x => x.Product.Id == productId);

                if (item is null)
                    return null;

                item.Quantity += amount;

                await SaveLocalCartAsync(localCart);

                CartChanged?.Invoke(this, EventArgs.Empty);

                return null;
            }

            var error = await _cartApiClient.IncreaseItemQuantityAsync(productId, amount);

            if (error is not null)
                return error;

            return await RefreshLocalCartAsync();
        }
        public async Task<ApiErrorResponseModel?> DecreaseItemQuantityAsync(int productId, int amount)
        {
            if (!IsAuthenticated) {
                var localCart = await GetLocalCartAsync();

                var item = localCart.Items.FirstOrDefault(x => x.Product.Id == productId);

                if (item is null)
                    return null;

                item.Quantity -= amount;

                if (item.Quantity <= 0)
                    localCart.Items.Remove(item);

                await SaveLocalCartAsync(localCart);

                CartChanged?.Invoke(this, EventArgs.Empty);

                return null;
            }

            var error = await _cartApiClient.DecreaseItemQuantityAsync(productId, amount);

            if (error is not null)
                return error;

            return await RefreshLocalCartAsync();
        }
        public async Task<ApiErrorResponseModel?> ClearAsync()
        {
            if (!IsAuthenticated) {
                await ClearLocalCartAsync();

                CartChanged?.Invoke(this, EventArgs.Empty);

                return null;
            }

            var error = await _cartApiClient.ClearCartAsync();

            if (error is not null)
                return error;

            return await RefreshLocalCartAsync();
        }
        private async Task ClearLocalCartAsync()
        {
            await _jsRuntime.InvokeVoidAsync("cartStorage.clear");
        }
        private async Task<ApiErrorResponseModel?> RefreshLocalCartAsync()
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
            if (!IsAuthenticated)
                return (null, null);

            var localCart = await GetLocalCartAsync();

            var (result, error) = await _cartApiClient.SynchronizeCartAsync(localCart);

            if (error != null)
                return (null, error);

            var refreshError = await RefreshLocalCartAsync();

            if (refreshError != null)
                return (null, refreshError);

            CartChanged?.Invoke(this, EventArgs.Empty);

            return (result, null);
        }
    }
}
