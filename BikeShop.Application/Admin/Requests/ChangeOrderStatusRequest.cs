using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Admin.Requests
{
    public sealed record ChangeOrderStatusRequest(int OrderId, OrderStatusDto OrderStatusDto);
}
