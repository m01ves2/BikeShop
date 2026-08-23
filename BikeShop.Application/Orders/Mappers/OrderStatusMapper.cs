using BikeShop.Application.Orders.DTOs;
using BikeShop.Domain.Entities;

namespace BikeShop.Application.Orders.Mappers
{
    public static class OrderStatusMapper
    {
        public static OrderStatusDto MapToDto(OrderStatus status) => status switch
        {
            OrderStatus.Paid => OrderStatusDto.Paid,
            OrderStatus.Pending => OrderStatusDto.Pending,
            OrderStatus.Shipped => OrderStatusDto.Shipped,
            OrderStatus.Completed => OrderStatusDto.Completed,
            OrderStatus.Cancelled => OrderStatusDto.Cancelled,
            _ => throw new Exception("Unexpected Order status")
        };

        public static OrderStatus MapFromDto(OrderStatusDto statusDto) => statusDto switch
        {
            OrderStatusDto.Paid => OrderStatus.Paid,
            OrderStatusDto.Pending => OrderStatus.Pending,
            OrderStatusDto.Shipped => OrderStatus.Shipped,
            OrderStatusDto.Completed => OrderStatus.Completed,
            OrderStatusDto.Cancelled => OrderStatus.Cancelled,
            _ => throw new Exception("Unexpected Order status")
        };
    }
}
