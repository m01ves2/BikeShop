using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.DTOs;

namespace BikeShop.Application.Products.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQueryHandler : IQueryHandler<GetProductsByCategoryIdQuery, Result<IReadOnlyList<ProductListItemDto>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByCategoryIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IReadOnlyList<ProductListItemDto>>> Handle(GetProductsByCategoryIdQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByCategoryIdAsync(query.CategoryId, cancellationToken);

            var result = products.Select(x => new ProductListItemDto(x.Id, x.Name, x.Price, x.Category.Name)).ToList();

            return Result<IReadOnlyList<ProductListItemDto>>.Success(result);
        }
    }
}
