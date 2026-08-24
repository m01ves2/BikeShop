using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Admin.DTOs;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Application.Orders.Mappers;

namespace BikeShop.Application.Admin.Orders.GetOrderDetails
{
    public class AdminGetOrderDetailsQueryHandler : IQueryHandler<AdminGetOrderDetailsQuery, Result<AdminOrderDetailsDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public AdminGetOrderDetailsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<AdminOrderDetailsDto>> Handle(AdminGetOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(query.OrderId, cancellationToken);

            if (order is null)
                return Result<AdminOrderDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Order with id = {query.OrderId} not found."));

            var resultData = new AdminOrderDetailsDto(
                 order.Id,
                 OrderStatusMapper.MapToDto(order.Status),
                 order.CreatedAt,
                 order.DeliveryAt,
                 order.DeliveryAddress,
                 order.CustomerPhone,
                 order.CourierPhone,
                 order.Items.Select(i =>
                     new OrderItemDto(
                         i.Id,
                         i.ProductName,
                         i.UnitPrice,
                         i.Quantity)),
                 order.GetAllowedStatuses()
                     .Select(OrderStatusMapper.MapToDto)
                     .ToList()
 );
            return Result<AdminOrderDetailsDto>.Success(resultData);
        }
    }
}
