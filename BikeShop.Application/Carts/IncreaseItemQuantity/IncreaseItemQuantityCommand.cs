using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.IncreaseItemQuantity
{
    public sealed record IncreaseItemQuantityCommand(int ApplicationUserId, int productId, int amount) : ICommand<Result>;
}
