using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.MergeGuestCart
{
    public sealed record MergeGuestCartCommand(int ApplicationUserId, CartDto GuestCart) : ICommand<Result<MergeGuestCartResultDto>>;
}
