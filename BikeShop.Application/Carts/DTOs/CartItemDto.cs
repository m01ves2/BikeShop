namespace BikeShop.Application.Carts.DTOs
{
    public sealed record CartItemDto(int Id, int Quantity, ProductCartDto Product);
}
