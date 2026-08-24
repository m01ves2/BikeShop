using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Admin.Orders.ChangeOrderStatus
{
    public sealed record AdminChangeOrderStatusCommand(int OrderId, OrderStatusDto OrderStatusDto) : ICommand<Result>;
}
