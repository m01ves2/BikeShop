using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Categories.CreateCategory;
using BikeShop.Application.Categories.DeleteCategory;
using BikeShop.Application.Categories.DTOs;
using BikeShop.Application.Categories.GetCategories;
using BikeShop.Application.Categories.GetCategoryDetails;
using BikeShop.Application.Categories.UpdateCategory;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.DTOs;
using BikeShop.Application.Products.GetProductsByCategoryId;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryListItemDto>>> _getCategoriesQueryHandler;
        private readonly IQueryHandler<GetProductsByCategoryIdQuery, Result<IReadOnlyList<ProductListItemDto>>> _getProductsbyCategoryIdQueryHandler;
        private readonly IQueryHandler<GetCategoryDetailsQuery, Result<CategoryDetailsDto>> _getCategoryDetailsQueryHandler;

        private readonly ICommandHandler<CreateCategoryCommand, Result> _createCategoryCommandHandler;
        private readonly ICommandHandler<UpdateCategoryCommand, Result> _updateCategoryCommandHandler;
        private readonly ICommandHandler<DeleteCategoryCommand, Result> _deleteCategoryCommandHandler;

        public CategoriesController(
            IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryListItemDto>>> getCategoriesQueryHandler,
            IQueryHandler<GetProductsByCategoryIdQuery, Result<IReadOnlyList<ProductListItemDto>>> getProductsbyCategoryIdQueryHandler,
            IQueryHandler<GetCategoryDetailsQuery, Result<CategoryDetailsDto>> getCategoryDetailsQueryHandler,
            ICommandHandler<CreateCategoryCommand, Result> createCategoryCommandHandler,
            ICommandHandler<UpdateCategoryCommand, Result> updateCategoryCommandHandler,
            ICommandHandler<DeleteCategoryCommand, Result> deleteCategoryCommandHandler)
        {
            _getCategoriesQueryHandler = getCategoriesQueryHandler;
            _getProductsbyCategoryIdQueryHandler = getProductsbyCategoryIdQueryHandler;
            _getCategoryDetailsQueryHandler = getCategoryDetailsQueryHandler;

            _createCategoryCommandHandler = createCategoryCommandHandler;
            _updateCategoryCommandHandler = updateCategoryCommandHandler;
            _deleteCategoryCommandHandler = deleteCategoryCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getCategoriesQueryHandler.Handle(new GetCategoriesQuery(), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _getCategoryDetailsQueryHandler.Handle(new GetCategoryDetailsQuery(id), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpGet("{categoryId}/products")]
        public async Task<IActionResult> GetProductsByCategory([FromRoute] int categoryId, CancellationToken cancellationToken)
        {
            var result = await _getProductsbyCategoryIdQueryHandler.Handle(new GetProductsByCategoryIdQuery(categoryId), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _createCategoryCommandHandler.Handle(command, cancellationToken);
            if(result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) {
                return BadRequest("Route id does not match body id.");
            }

            var result = await _updateCategoryCommandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) {
                return BadRequest("Route id does not match body id.");
            }

            var result = await _deleteCategoryCommandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }


    }
}
