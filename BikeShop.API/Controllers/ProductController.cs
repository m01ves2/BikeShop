using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.CreateProduct;
using BikeShop.Application.Products.DeleteProduct;
using BikeShop.Application.Products.GetProductDetails;
using BikeShop.Application.Products.GetProducts;
using BikeShop.Application.Products.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IQueryHandler<GetProductsQuery, Result<IReadOnlyList<ProductListItemDto>>> _getProductsQueryHandler;
        private readonly IQueryHandler<GetProductDetailsQuery, Result<ProductDetailsDto>> _getProductDetailsQueryHandler;
                         
        private readonly ICommandHandler<CreateProductCommand, Result> _createProductCommandHandler;
        private readonly ICommandHandler<UpdateProductCommand, Result> _updateProductCommandHandler;
        private readonly ICommandHandler<DeleteProductCommand, Result> _deleteProductCommandHandler;

        public ProductController(
            IQueryHandler<GetProductsQuery, Result<IReadOnlyList<ProductListItemDto>>> getProductsQueryHandler,
            IQueryHandler<GetProductDetailsQuery, Result<ProductDetailsDto>> getProductDetailsQueryHandler,
            ICommandHandler<CreateProductCommand, Result> createProductCommandHandler,
            ICommandHandler<UpdateProductCommand, Result> updateProductCommandHandler,
            ICommandHandler<DeleteProductCommand, Result> deleteProductCommandHandler)
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
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _getProductDetailsQueryHandler.Handle(new GetProductDetailsQuery(id), cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _createProductCommandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) {
                return BadRequest("Route id does not match body id.");
            }

            var result = await _updateProductCommandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, DeleteProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) {
                return BadRequest("Route id does not match body id.");
            }

            var result = await _deleteProductCommandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

    }
}
