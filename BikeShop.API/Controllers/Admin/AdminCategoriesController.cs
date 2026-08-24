using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Admin.Categories.CreateCategory;
using BikeShop.Application.Admin.Categories.DeleteCategory;
using BikeShop.Application.Admin.Categories.UpdateCategory;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/categories")]
    public class AdminCategoriesController : ControllerBase
    {
        private readonly ICommandHandler<AdminCreateCategoryCommand, Result> _createCategoryCommandHandler;
        private readonly ICommandHandler<AdminUpdateCategoryCommand, Result> _updateCategoryCommandHandler;
        private readonly ICommandHandler<AdminDeleteCategoryCommand, Result> _deleteCategoryCommandHandler;

        public AdminCategoriesController(
            ICommandHandler<AdminCreateCategoryCommand, Result> createCategoryCommandHandler,
            ICommandHandler<AdminUpdateCategoryCommand, Result> updateCategoryCommandHandler,
            ICommandHandler<AdminDeleteCategoryCommand, Result> deleteCategoryCommandHandler)
        {
            _createCategoryCommandHandler = createCategoryCommandHandler;
            _updateCategoryCommandHandler = updateCategoryCommandHandler;
            _deleteCategoryCommandHandler = deleteCategoryCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _createCategoryCommandHandler.Handle(command, cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, AdminUpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) {
                return this.ToActionResult(new Error(ErrorCode.Validation, "Route id does not match body id."));
            }

            var result = await _updateCategoryCommandHandler.Handle(command, cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _deleteCategoryCommandHandler.Handle(new AdminDeleteCategoryCommand(id), cancellationToken);

            if (result.IsFailure)
                return this.ToActionResult(result.Error!);

            return NoContent();
        }
    }
}
