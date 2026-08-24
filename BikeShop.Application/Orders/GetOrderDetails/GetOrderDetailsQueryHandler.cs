using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Application.Orders.Mappers;

namespace BikeShop.Application.Orders.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, Result<OrderDetailsDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Result<OrderDetailsDto>> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(query.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result<OrderDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {query.ApplicationUserId}"));

            var order = await _orderRepository.GetByIdForCustomerAsync(customer.Id, query.OrderId,  cancellationToken);

            if (order is null)
                return Result<OrderDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Order with id = {query.OrderId} not found."));

            var resultData = new OrderDetailsDto( 
                order.Id,
                OrderStatusMapper.MapToDto(order.Status),
                order.CreatedAt,
                order.DeliveryAt,
                order.DeliveryAddress,
                order.CustomerPhone,
                order.CourierPhone,
                order.Items.Select(i => new OrderItemDto(i.Id, i.ProductName, i.UnitPrice, i.Quantity))
                );
            return Result<OrderDetailsDto>.Success(resultData);
        }
    }
}
