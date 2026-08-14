using BikeShop.Blazor.Models;

namespace BikeShop.Blazor.Services
{
    public interface ICartService
    {
        event EventHandler? CartChanged;

        Task<CartModel> GetCartAsync();
        Task AddItemAsync(CartItemModel item);
        Task RemoveItemAsync(int productId);
        Task UpdateQuantityAsync(int productId, int quantity);
        Task ClearAsync();
    }
}
