using BikeShop.Domain.Entities;

namespace BikeShop.Application.Carts.DTOs
{
    public sealed record CartDto(List<CartItemDto> Items);
}
