using BikeShop.Domain.Entities;

namespace BikeShop.Application.Orders.DTOs
{
    public sealed record OrderListItemDto(int Id, OrderStatusDto Status, DateTime CreatedAt);
}
