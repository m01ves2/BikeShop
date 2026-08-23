using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Orders.CancelOrder
{
    public sealed record CancelOrderCommand(int ApplicationUserId, int OrderId) : ICommand<Result>;
}
