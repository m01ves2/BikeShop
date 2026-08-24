using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Orders.UpdateOrder
{
    public class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result<OrderDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            var order = await _orderRepository.GetByIdForCustomerAsync(customer.Id, command.OrderId, cancellationToken);
            if (order is null)
                return Result<OrderDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Order with id = {command.OrderId} not found."));

            order.ChangeDeliveryAddress(command.DeliveryAddress);
            order.ChangeDeliveryAt(command.DeliveryAt);
            order.ChangeCustomerPhone(command.CustomerPhone);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
