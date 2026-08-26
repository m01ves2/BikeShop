using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.DTOs;

namespace BikeShop.Application.Products.GetProducts
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Result<IReadOnlyList<ProductListItemDto>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IReadOnlyList<ProductListItemDto>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            var resultData = products.Select(x => new ProductListItemDto(x.Id, x.Name,  x.Price, x.StockQuantity, x.Category.Name)).ToList();

            return Result<IReadOnlyList<ProductListItemDto>>.Success(resultData);
        }
    }
}
