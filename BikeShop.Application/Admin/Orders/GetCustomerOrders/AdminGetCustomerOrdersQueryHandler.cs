using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Application.Orders.Mappers;

namespace BikeShop.Application.Admin.Orders.GetCustomerOrders
{
    public class AdminGetCustomerOrdersQueryHandler : IQueryHandler<AdminGetCustomerOrdersQuery, Result<IReadOnlyList<OrderListItemDto>>>
    {
        private readonly ICustomerRepository _customerRepository;

        public AdminGetCustomerOrdersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<IReadOnlyList<OrderListItemDto>>> Handle(AdminGetCustomerOrdersQuery query, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithOrdersByIdAsync(query.CustomerId, cancellationToken);

            if (customer is null)
                return Result<IReadOnlyList<OrderListItemDto>>.Failure(
                    new Error(ErrorCode.NotFound, $"Customer with id = {query.CustomerId} not found."));

            var resultData = customer.Orders
                .Select(order => new OrderListItemDto(
                    order.Id,
                    OrderStatusMapper.MapToDto(order.Status),
                    order.CreatedAt))
                .ToList();

            return Result<IReadOnlyList<OrderListItemDto>>.Success(resultData);
        }
    }
}
