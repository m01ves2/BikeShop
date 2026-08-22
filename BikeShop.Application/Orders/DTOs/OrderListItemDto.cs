using BikeShop.Domain.Entities;

namespace BikeShop.Application.Orders.DTOs
{
    public enum OrderStatusDto
    {
        Pending,
        Paid,
        Shipped,
        Completed,
        Cancelled,
    }
    public sealed record OrderListItemDto(int Id, OrderStatusDto Status, DateTime CreatedAt);
}
