using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.ClearCart
{
    public class ClearCartCommandHandler : ICommandHandler<ClearCartCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public ClearCartCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<Result> Handle(ClearCartCommand command, CancellationToken cancellationToken)
        {

            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            var cart = customer.Cart;

            cart.Clear();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
