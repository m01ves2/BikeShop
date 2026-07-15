using BikeShop.Application.Categories.GetCategories;

namespace BikeShop.Application.Products.GetProductDetails
{
    public sealed record ProductDetailsDto(int Id, string Name, string Description, decimal Price, int StockQuantity, int CategoryId, string CategoryName);
}
