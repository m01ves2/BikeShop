using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Domain.Entities;

namespace BikeShop.Application.Orders.CancelOrder
{
    public class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result<OrderDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            var order = await _orderRepository.GetByIdAsync(customer.Id, command.OrderId, cancellationToken);
            if (order is null)
                return Result<OrderDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Order with id = {command.OrderId} not found."));

            order.ChangeStatus(OrderStatus.Cancelled);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
