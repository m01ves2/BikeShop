using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Categories.DTOs;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.GetProductDetails;

namespace BikeShop.Application.Categories.GetCategoryDetails
{
    public class GetCategoryDetailsQueryHandler : IQueryHandler<GetCategoryDetailsQuery, Result<CategoryDetailsDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryDetailsQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryDetailsDto>> Handle(GetCategoryDetailsQuery query, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(query.Id, cancellationToken);

            if (category == null)
                return Result<CategoryDetailsDto>.Failure(new Error(ErrorCode.NotFound, $"Not found product id = {query.Id}"));

            var result = new CategoryDetailsDto(category.Id, category.Name);

            return Result<CategoryDetailsDto>.Success(result);
        }
    }
}
