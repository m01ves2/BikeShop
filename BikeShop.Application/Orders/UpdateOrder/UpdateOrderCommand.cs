using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Orders.UpdateOrder
{
    public sealed record UpdateOrderCommand(int ApplicationUserId, int OrderId, string DeliveryAddress, DateTime DeliveryAt, string CustomerPhone) : ICommand<Result>;
}
