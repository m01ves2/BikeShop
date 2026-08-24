using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Admin.Orders.GetCustomerOrders
{
    public sealed record AdminGetCustomerOrdersQuery(int CustomerId)  : IQuery<Result<IReadOnlyList<OrderListItemDto>>>;
}
