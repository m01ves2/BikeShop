namespace BikeShop.Blazor.Models
{
    public sealed record SynchronizeCartResultModel(CartModel Cart, List<string> RemovedProductNames);
}
