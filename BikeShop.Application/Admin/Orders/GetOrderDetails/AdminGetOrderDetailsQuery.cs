using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Admin.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Admin.Orders.GetOrderDetails
{
    public sealed record AdminGetOrderDetailsQuery(int OrderId) : IQuery<Result<AdminOrderDetailsDto>>;
}
