using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.ClearCart
{
    public sealed record ClearCartCommand(int ApplicationUserId) : ICommand<Result>;
}
