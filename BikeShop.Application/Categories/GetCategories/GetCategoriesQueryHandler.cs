using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Categories.GetCategories
{
    public sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryListItemDto>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<IReadOnlyList<CategoryListItemDto>>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken) //GetCategoriesQuery query -"намерение выполнить этот сценарий"
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);

            var result = categories.Select(x => new CategoryListItemDto(x.Id, x.Name)).ToList();

            return Result<IReadOnlyList<CategoryListItemDto>>.Success(result);
        }
    }
}
