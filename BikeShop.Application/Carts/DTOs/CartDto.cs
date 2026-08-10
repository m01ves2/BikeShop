using BikeShop.Domain.Entities;

namespace BikeShop.Application.Carts.DTOs
{
    public sealed record CartDto(int Id, int CustomerId, List<CartItemDto> Items);
}
