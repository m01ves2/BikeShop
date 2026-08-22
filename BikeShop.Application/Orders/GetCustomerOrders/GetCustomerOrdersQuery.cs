using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Orders.GetCustomerOrders
{
    public sealed record GetCustomerOrdersQuery(int ApplicationUserId) : IQuery<Result<IReadOnlyList<OrderListItemDto>>>;
}
