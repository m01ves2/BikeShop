using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Orders.Requests
{
    public sealed record ChangeOrderStatusRequest(int OrderId, OrderStatusDto OrderStatusDto);
}
