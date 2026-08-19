using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services
{
    public interface ICartService
    {
        event EventHandler? CartChanged;

        Task<CartModel> GetLocalCartAsync();

        Task<(CartModel?, ApiErrorResponseModel?)> GetServerCartAsync();
        Task<ApiErrorResponseModel?> AddItemAsync(CartItemModel item);
        Task<ApiErrorResponseModel?> RemoveItemAsync(int productId);
        Task<ApiErrorResponseModel?> IncreaseItemQuantityAsync(int productId, int amount);
        Task<ApiErrorResponseModel?> DecreaseItemQuantityAsync(int productId, int amount);
        Task<ApiErrorResponseModel?> ClearAsync();

        Task<(SynchronizeCartResultModel?, ApiErrorResponseModel?)> SynchronizeCartAsync();
    }
}
