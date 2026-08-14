using System.Text.Json;
using BikeShop.Blazor.Models;
using Microsoft.JSInterop;

namespace BikeShop.Blazor.Services
{
    public class CartService : ICartService
    {
        private readonly IJSRuntime _jsRuntime;

        public event EventHandler? CartChanged;

        public CartService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<CartModel> GetCartAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string?>("cartStorage.get");

            if (string.IsNullOrEmpty(json)) {
                return new CartModel(new List<CartItemModel>());
            }

            return JsonSerializer.Deserialize<CartModel>(json) ?? new CartModel(new List<CartItemModel>());
        }

        public async Task AddItemAsync(CartItemModel item)
        {
            var cart = await GetCartAsync();
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
            var cart = await GetCartAsync();

            cart.Items.RemoveAll(i => i.Product.Id == productId);

            await SaveAsync(cart);
            CartChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task UpdateQuantityAsync(int productId, int quantity)
        {
            var cart = await GetCartAsync();
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

        private async Task SaveAsync(CartModel cart)
        {
            var json = JsonSerializer.Serialize(cart);

            await _jsRuntime.InvokeVoidAsync("cartStorage.set", json);
        }
    }
}
