using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.AddItem
{
    public class AddItemCommandHandler : ICommandHandler<AddItemCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public AddItemCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork) 
        { 
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<Result> Handle(AddItemCommand command, CancellationToken cancellationToken)
        {
            //var customer = await _customerRepository.GetCustomerByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);
            //if (customer == null) {
            //    return Result.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));
            //}

            //var cart = await _cartRepository.GetByCustomerIdAsync(customer.Id, cancellationToken);

            //if (cart is null) {
            //    return Result.Failure(new Error(ErrorCode.NotFound, $"Not found cart with customerId = {customer.Id}"));
            //}
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            var cart = customer.Cart;

            cart.AddItem(command.ProductId, 1);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
