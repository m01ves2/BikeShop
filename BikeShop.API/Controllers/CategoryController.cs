using System.Threading;
using BikeShop.Application.Categories.CreateCategory;
using BikeShop.Application.Categories.GetCategories;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly GetCategoriesQueryHandler _getCategoriesQueryHandler;
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;

        public CategoryController(GetCategoriesQueryHandler getCategoriesQueryHandler, CreateCategoryCommandHandler createCategoryCommandHandler)
        {
            _getCategoriesQueryHandler = getCategoriesQueryHandler;
            _createCategoryCommandHandler = createCategoryCommandHandler;
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
    }
}
