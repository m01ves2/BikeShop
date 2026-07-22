using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Categories.CreateCategory;
using BikeShop.Application.Categories.DeleteCategory;
using BikeShop.Application.Categories.GetCategories;
using BikeShop.Application.Categories.UpdateCategory;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryListItemDto>>> _getCategoriesQueryHandler;

        private readonly ICommandHandler<CreateCategoryCommand, Result> _createCategoryCommandHandler;
        private readonly ICommandHandler<UpdateCategoryCommand, Result> _updateCategoryCommandHandler;
        private readonly ICommandHandler<DeleteCategoryCommand, Result> _deleteCategoryCommandHandler;

        public CategoriesController(
            IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryListItemDto>>> getCategoriesQueryHandler, 
            ICommandHandler<CreateCategoryCommand, Result> createCategoryCommandHandler,
            ICommandHandler<UpdateCategoryCommand, Result> updateCategoryCommandHandler,
            ICommandHandler<DeleteCategoryCommand, Result> deleteCategoryCommandHandler)
        {
            _getCategoriesQueryHandler = getCategoriesQueryHandler;
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
