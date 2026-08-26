namespace BikeShop.Blazor.Models.Cart
{
    public sealed record SynchronizeCartResultModel(CartModel Cart, List<string> RemovedProductNames);
}
