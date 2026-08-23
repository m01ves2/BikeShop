using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.Mappers;

namespace BikeShop.Application.Orders.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandHandler : ICommandHandler<ChangeOrderStatusCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeOrderStatusCommandHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ChangeOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            var order = await _orderRepository.GetByIdAsync(customer.Id, command.OrderId, cancellationToken);
            if (order is null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Order with id = {command.OrderId} not found."));

            order.ChangeStatus( OrderStatusMapper.MapFromDto(command.OrderStatusDto));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
