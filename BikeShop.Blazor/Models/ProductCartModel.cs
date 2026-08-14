namespace BikeShop.Blazor.Models
{
    public sealed record ProductCartModel(int Id, string Name, decimal Price, int StockQuantity);
}
