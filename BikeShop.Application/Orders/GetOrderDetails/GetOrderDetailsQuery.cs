using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Orders.GetOrderDetails
{
    public sealed record GetOrderDetailsQuery(int ApplicationUserId, int OrderId) : IQuery<Result<OrderDetailsDto>>;
}
