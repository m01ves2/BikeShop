using System.Threading;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Categories.CreateCategory;
using BikeShop.Application.Categories.GetCategories;
using BikeShop.Application.Categories.UpdateCategory;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryDto>>> _getCategoriesQueryHandler;

        private readonly ICommandHandler<CreateCategoryCommand, Result> _createCategoryCommandHandler;
        private readonly ICommandHandler<UpdateCategoryCommand, Result> _updateCategoryCommandHandler;

        public CategoryController(
            IQueryHandler<GetCategoriesQuery, Result<IReadOnlyList<CategoryDto>>> getCategoriesQueryHandler, 
            ICommandHandler<CreateCategoryCommand, Result> createCategoryCommandHandler,
            ICommandHandler<UpdateCategoryCommand, Result> updateCategoryCommandHandler)
        {
            _getCategoriesQueryHandler = getCategoriesQueryHandler;
            _createCategoryCommandHandler = createCategoryCommandHandler;
            _updateCategoryCommandHandler = updateCategoryCommandHandler;
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
        public async Task<IActionResult> Update(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _updateCategoryCommandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
