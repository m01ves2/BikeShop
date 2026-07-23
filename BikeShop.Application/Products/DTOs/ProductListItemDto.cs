namespace BikeShop.Application.Products.DTOs
{
    public sealed record ProductListItemDto(int Id, string Name, decimal Price, string CategoryName);
}