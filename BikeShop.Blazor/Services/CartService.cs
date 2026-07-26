using System.Text.Json;
using BikeShop.Blazor.Models;
using Microsoft.JSInterop;

namespace BikeShop.Blazor.Services
{
    public class CartService : ICartService
    {
        private readonly IJSRuntime _jsRuntime;

        private const string StorageKey = "cart";

        public CartService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<List<CartItemModel>> GetItemsAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string?>("cartStorage.get");

            if (string.IsNullOrEmpty(json)) {
                return new List<CartItemModel>();
            }

            return JsonSerializer.Deserialize<List<CartItemModel>>(json) ?? new List<CartItemModel>();
        }

        public async Task AddItemAsync(CartItemModel item)
        {
            var items = await GetItemsAsync();
            var existing = items.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existing != null) {
                existing.Quantity += item.Quantity;
            }
            else {
                items.Add(item);
            }

            await SaveAsync(items);
        }

        public async Task RemoveItemAsync(int productId)
        {
            var items = await GetItemsAsync();

            items.RemoveAll(x => x.ProductId == productId);

            await SaveAsync(items);
        }

        public async Task UpdateQuantityAsync(int productId, int quantity)
        {
            var items = await GetItemsAsync();
            var item = items.FirstOrDefault(x => x.ProductId == productId);

            if (item == null) {
                return;
            }

            item.Quantity = quantity;
            await SaveAsync(items);
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("cartStorage.clear");
        }

        private async Task SaveAsync(List<CartItemModel> items)
        {
            var json = JsonSerializer.Serialize(items);

            await _jsRuntime.InvokeVoidAsync("cartStorage.set", json);
        }
    }
}
