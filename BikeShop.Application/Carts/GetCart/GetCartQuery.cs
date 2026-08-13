using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.GetCartByCustomerId
{
    public sealed record GetCartQuery(int ApplicationUserId) : IQuery<Result<CartDto>>;
}
