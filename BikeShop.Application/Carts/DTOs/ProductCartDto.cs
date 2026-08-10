namespace BikeShop.Application.Carts.DTOs
{
    public sealed record ProductCartDto(int Id, string Name, decimal Price, int StockQuantity);
}
