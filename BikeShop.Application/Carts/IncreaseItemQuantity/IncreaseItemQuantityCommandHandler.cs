using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.IncreaseItemQuantity
{
    public class IncreaseItemQuantityCommandHandler : ICommandHandler<IncreaseItemQuantityCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public IncreaseItemQuantityCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }
        public async Task<Result> Handle(IncreaseItemQuantityCommand command, CancellationToken cancellationToken)
        {

            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            var cart = customer.Cart;

            cart.IncreaseItemQuantity(command.productId, command.amount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
