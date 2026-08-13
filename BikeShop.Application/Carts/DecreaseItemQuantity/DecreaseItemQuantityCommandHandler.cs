using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.DecreaseItemQuantity
{
    public class DecreaseItemQuantityCommandHandler : ICommandHandler<DecreaseItemQuantityCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DecreaseItemQuantityCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }
        public async Task<Result> Handle(DecreaseItemQuantityCommand command, CancellationToken cancellationToken)
        {

            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            var cart = customer.Cart;

            cart.DecreaseItemQuantity(command.productId, command.amount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
