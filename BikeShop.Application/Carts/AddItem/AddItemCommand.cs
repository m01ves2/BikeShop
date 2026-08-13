using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.AddItem
{
    public sealed record AddItemCommand(int ApplicationUserId, int ProductId) : ICommand<Result>;
}
