using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Admin.Products.CreateProduct;
using BikeShop.Application.Admin.Products.DeleteProduct;
using BikeShop.Application.Admin.Products.UpdateProduct;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.DTOs;
using BikeShop.Application.Products.GetProductDetails;
using BikeShop.Application.Products.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IQueryHandler<GetProductsQuery, Result<IReadOnlyList<ProductListItemDto>>> _getProductsQueryHandler;
        private readonly IQueryHandler<GetProductDetailsQuery, Result<ProductDetailsDto>> _getProductDetailsQueryHandler;
                         
        private readonly ICommandHandler<AdminCreateProductCommand, Result> _createProductCommandHandler;
        private readonly ICommandHandler<AdminUpdateProductCommand, Result> _updateProductCommandHandler;
        private readonly ICommandHandler<AdminDeleteProductCommand, Result> _deleteProductCommandHandler;

        public ProductsController(
            IQueryHandler<GetProductsQuery, Result<IReadOnlyList<ProductListItemDto>>> getProductsQueryHandler,
            IQueryHandler<GetProductDetailsQuery, Result<ProductDetailsDto>> getProductDetailsQueryHandler,
            ICommandHandler<AdminCreateProductCommand, Result> createProductCommandHandler,
            ICommandHandler<AdminUpdateProductCommand, Result> updateProductCommandHandler,
            ICommandHandler<AdminDeleteProductCommand, Result> deleteProductCommandHandler)
        {
            _getProductsQueryHandler = getProductsQueryHandler;
            _getProductDetailsQueryHandler = getProductDetailsQueryHandler;
            _createProductCommandHandler = createProductCommandHandler;
            _updateProductCommandHandler = updateProductCommandHandler;
            _deleteProductCommandHandler = deleteProductCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getProductsQueryHandler.Handle(new GetProductsQuery(), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error?.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _getProductDetailsQueryHandler.Handle(new GetProductDetailsQuery(id), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _createProductCommandHandler.Handle(command, cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, AdminUpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) {
                return this.ToActionResult(new Error(ErrorCode.Validation, "Route id does not match body id."));
            }

            var result = await _updateProductCommandHandler.Handle(command, cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, AdminDeleteProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) {
                return this.ToActionResult(new Error(ErrorCode.Validation, "Route id does not match body id."));
            }

            var result = await _deleteProductCommandHandler.Handle(command, cancellationToken);


            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return NoContent();
        }

    }
}
