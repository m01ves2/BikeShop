using BikeShop.Application.Categories.GetCategories;

namespace BikeShop.Application.Products.DTOs
{
    public sealed record ProductDetailsDto(int Id, string Name, string Description, decimal Price, int StockQuantity, int CategoryId, string CategoryName);
}
