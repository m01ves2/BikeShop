using BikeShop.Application.Categories.GetCategories;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly GetCategoriesQueryHandler _handler;

        public CategoriesController(GetCategoriesQueryHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _handler.Handle(new GetCategoriesQuery(), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }
    }
}
