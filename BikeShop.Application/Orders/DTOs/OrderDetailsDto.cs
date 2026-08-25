using BikeShop.Domain.Entities;

namespace BikeShop.Application.Orders.DTOs
{
    public sealed record OrderDetailsDto(
        int Id, 
        OrderStatusDto Status, 
        DateTime CreatedAt, 
        DateTime DeliveryAt, 
        string DeliveryAddress, 
        string CustomerPhone, 
        string? CourierPhone,
        bool CanBeEdited,
        bool CanBeCancelled,
        IEnumerable<OrderItemDto> items);
}
