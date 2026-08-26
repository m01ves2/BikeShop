namespace BikeShop.Blazor.Models.Cart
{
    public sealed record ProductCartModel(int Id, string Name, decimal Price, int StockQuantity);
}
