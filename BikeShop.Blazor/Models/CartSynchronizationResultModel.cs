namespace BikeShop.Blazor.Models
{
    public sealed record CartSynchronizationResultModel(CartModel Cart, List<string> RemovedProductNames);
}
