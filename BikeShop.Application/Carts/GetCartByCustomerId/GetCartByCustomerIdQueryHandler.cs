using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Categories.DTOs;
using BikeShop.Application.Common.Models;
using BikeShop.Domain.Entities;

namespace BikeShop.Application.Carts.GetCartByCustomerId
{
    public class GetCartByCustomerIdQueryHandler : IQueryHandler<GetCartByCustomerIdQuery, Result<CartDto>>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartByCustomerIdQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Result<CartDto>> Handle(GetCartByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(query.CustomerId, cancellationToken);

            if (cart == null)
                return Result<CartDto>.Failure(new Error(ErrorCode.NotFound, $"Not found cart with CustomerId = {query.CustomerId}"));

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
