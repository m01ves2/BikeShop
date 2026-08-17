using System.Text.Json;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.ApiClients;
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

        public async Task AddItemAsync(CartItemModel item)
        {
            var cart = await GetLocalCartAsync();
            var existing = cart.Items.FirstOrDefault(i => i.Product.Id == item.Product.Id);

            if (existing != null) {
                existing.Quantity += item.Quantity;
            }
            else {
                cart.Items.Add(item);
            }

            await SaveAsync(cart);
            CartChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task RemoveItemAsync(int productId)
        {
            var cart = await GetLocalCartAsync();

            cart.Items.RemoveAll(i => i.Product.Id == productId);

            await SaveAsync(cart);
            CartChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task UpdateQuantityAsync(int productId, int quantity)
        {
            var cart = await GetLocalCartAsync();
            var existing = cart.Items.FirstOrDefault(x => x.Product.Id == productId);

            if (existing == null) {
                return;
            }

            existing.Quantity = quantity;
            await SaveAsync(cart);
            CartChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("cartStorage.clear");
            CartChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task<CartModel?> GetServerCartAsync()
        {
            return await _cartApiClient.GetCartAsync();
        }

        public async Task<CartSynchronizationResultModel?> SynchronizeCartAsync()
        {
            var localCart = await GetLocalCartAsync();
            var resultCart = await _cartApiClient.SynchronizeCartAsync(localCart);

            //TODO обработка ошибок
            if (resultCart == null) {
                return new CartSynchronizationResultModel(localCart, new List<string>());
            }

            //await ClearAsync();
            await SaveAsync(resultCart.cart);
            CartChanged?.Invoke(this, EventArgs.Empty);
            return new CartSynchronizationResultModel(localCart, resultCart.RemovedProductNames);
        }

        private async Task SaveAsync(CartModel cart)
        {
            var json = JsonSerializer.Serialize(cart);

            await _jsRuntime.InvokeVoidAsync("cartStorage.set", json);
        }
    }
}
