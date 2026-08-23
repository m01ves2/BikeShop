using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Application.Orders.Mappers;

namespace BikeShop.Application.Orders.GetCustomerOrders
{
    public class GetCustomerOrdersQueryHandler : IQueryHandler<GetCustomerOrdersQuery, Result<IReadOnlyList<OrderListItemDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerOrdersQueryHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Result<IReadOnlyList<OrderListItemDto>>> Handle(GetCustomerOrdersQuery query, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithOrdersByApplicationUserIdAsync(query.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result<IReadOnlyList<OrderListItemDto>>.Failure(
                            new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {query.ApplicationUserId}"));

            var orders = customer.Orders;
            
            var resultData = new List<OrderListItemDto>();
            foreach (var order in orders) {
                var orderDto = new OrderListItemDto(order.Id, OrderStatusMapper.MapToDto(order.Status), order.CreatedAt);
                resultData.Add(orderDto);
            }
            return Result<IReadOnlyList<OrderListItemDto>>.Success(resultData);
        }
    }
}
