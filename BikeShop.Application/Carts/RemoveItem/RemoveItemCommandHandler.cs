using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Carts.AddItem;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.RemoveItem
{
    public class RemoveItemCommandHandler : ICommandHandler<RemoveItemCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public RemoveItemCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            var cart = customer.Cart;

            cart.RemoveItem(command.ProductId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
