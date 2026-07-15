namespace BikeShop.Application.Products.GetProducts
{
    public sealed record ProductListItemDto(int Id, string Name, decimal Price, string CategoryName);
}