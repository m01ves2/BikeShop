namespace BikeShop.Application.Carts.DTOs
{
    public sealed record SynchronizeCartResultDto(CartDto cart, List<string> RemovedProductNames);
}
