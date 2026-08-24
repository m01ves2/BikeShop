using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Admin.Products.CreateProduct;
using BikeShop.Application.Admin.Products.DeleteProduct;
using BikeShop.Application.Admin.Products.UpdateProduct;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/products")]
    public class AdminProductsController : ControllerBase
    {
        private readonly ICommandHandler<AdminCreateProductCommand, Result> _createProductCommandHandler;
        private readonly ICommandHandler<AdminUpdateProductCommand, Result> _updateProductCommandHandler;
        private readonly ICommandHandler<AdminDeleteProductCommand, Result> _deleteProductCommandHandler;

        public AdminProductsController(
            ICommandHandler<AdminCreateProductCommand, Result> createProductCommandHandler,
            ICommandHandler<AdminUpdateProductCommand, Result> updateProductCommandHandler,
            ICommandHandler<AdminDeleteProductCommand, Result> deleteProductCommandHandler)
        {
            _createProductCommandHandler = createProductCommandHandler;
            _updateProductCommandHandler = updateProductCommandHandler;
            _deleteProductCommandHandler = deleteProductCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _createProductCommandHandler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return this.ToActionResult(result.Error!);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, AdminUpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) {
                return this.ToActionResult(new Error(ErrorCode.Validation, "Route id does not match body id."));
            }

            var result = await _updateProductCommandHandler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return this.ToActionResult(result.Error!);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _deleteProductCommandHandler.Handle(new AdminDeleteProductCommand(id), cancellationToken);

            if (result.IsFailure)
                return this.ToActionResult(result.Error!);

            return NoContent();
        }
    }
}