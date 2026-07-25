using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.DTOs;

namespace BikeShop.Application.Products.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IQueryHandler<GetProductDetailsQuery, Result<ProductDetailsDto>>
    {

        private readonly IProductRepository _productRepository;

        public GetProductDetailsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductDetailsDto>> Handle(GetProductDetailsQuery query, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(query.Id, cancellationToken);

            if (product == null)
                return Result<ProductDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Not found product id = {query.Id}"));

            var result = new ProductDetailsDto(product.Id, product.Name, product.Description, product.Price, product.StockQuantity,
                                                product.Category.Id, product.Category.Name);

            return Result<ProductDetailsDto>.Success(result);
        }
    }
}
