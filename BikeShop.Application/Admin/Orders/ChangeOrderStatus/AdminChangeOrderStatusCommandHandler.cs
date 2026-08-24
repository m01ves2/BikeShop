using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.Mappers;

namespace BikeShop.Application.Admin.Orders.ChangeOrderStatus
{
    public class AdminChangeOrderStatusCommandHandler : ICommandHandler<AdminChangeOrderStatusCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminChangeOrderStatusCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AdminChangeOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);
            if (order is null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Order with id = {command.OrderId} not found."));

            order.ChangeStatus( OrderStatusMapper.MapFromDto(command.OrderStatusDto));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
