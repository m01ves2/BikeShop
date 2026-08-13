using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.DecreaseItemQuantity
{
    public sealed record DecreaseItemQuantityCommand(int ApplicationUserId, int productId, int amount) : ICommand<Result>;
}
