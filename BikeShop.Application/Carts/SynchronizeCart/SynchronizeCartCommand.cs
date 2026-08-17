using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.SynchronizeCart
{
    public sealed record SynchronizeCartCommand(int ApplicationUserId, CartDto LocalCart) : ICommand<Result<SynchronizeCartResultDto>>;
}
