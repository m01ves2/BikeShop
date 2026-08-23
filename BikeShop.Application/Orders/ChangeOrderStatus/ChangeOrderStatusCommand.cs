using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Orders.ChangeOrderStatus
{
    public sealed record ChangeOrderStatusCommand(int ApplicationUserId, int OrderId, OrderStatusDto OrderStatusDto) : ICommand<Result>;
}
