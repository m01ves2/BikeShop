using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.GetCartByCustomerId
{
    public class GetCartQueryHandler : IQueryHandler<GetCartQuery, Result<CartDto>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCartQueryHandler(ICartRepository cartRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<CartDto>> Handle(GetCartQuery query, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(query.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result<CartDto>.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {query.ApplicationUserId}"));

            var cart = customer.Cart;

            var resultData = new CartDto(
                cart.Id,
                cart.CustomerId,
                cart.Items.Select(i => new CartItemDto(
                    i.Id,
                    i.Quantity,
                    new ProductCartDto(
                        i.Product.Id,
                        i.Product.Name,
                        i.Product.Price,
                        i.Product.StockQuantity
                    ))).ToList()
                );

            return Result<CartDto>.Success(resultData);
        }
    }
}
