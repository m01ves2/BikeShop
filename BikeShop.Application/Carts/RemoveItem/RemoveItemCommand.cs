using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.RemoveItem
{
    public sealed record RemoveItemCommand(int ApplicationUserId, int ProductId) : ICommand<Result>;
}
