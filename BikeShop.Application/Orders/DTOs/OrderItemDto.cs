namespace BikeShop.Application.Orders.DTOs
{
    public sealed record OrderItemDto(int Id, string ProductName, decimal UnitPrice, int Quantity);
}
