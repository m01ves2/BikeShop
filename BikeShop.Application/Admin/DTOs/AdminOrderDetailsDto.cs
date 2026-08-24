using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Admin.DTOs
{
    public sealed record AdminOrderDetailsDto(
        int Id,
        OrderStatusDto Status,
        DateTime CreatedAt,
        DateTime DeliveryAt,
        string DeliveryAddress,
        string CustomerPhone,
        string? CourierPhone,
        IEnumerable<OrderItemDto> Items,
        IEnumerable<OrderStatusDto> AllowedStatuses);
}
