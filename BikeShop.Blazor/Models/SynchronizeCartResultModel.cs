namespace BikeShop.Blazor.Models
{
    public sealed record SynchronizeCartResultModel(CartModel cart, List<string> RemovedProductNames);
}
