namespace BikeShop.Blazor.Models.Catalog
{
    public class ProductListItemModel
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public string CategoryName { get; init; } = string.Empty;
    }
}
