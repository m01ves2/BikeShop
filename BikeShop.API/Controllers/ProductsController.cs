using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
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
                        

        public ProductsController(
            IQueryHandler<GetProductsQuery, Result<IReadOnlyList<ProductListItemDto>>> getProductsQueryHandler,
            IQueryHandler<GetProductDetailsQuery, Result<ProductDetailsDto>> getProductDetailsQueryHandler)
        {
            _getProductsQueryHandler = getProductsQueryHandler;
            _getProductDetailsQueryHandler = getProductDetailsQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getProductsQueryHandler.Handle(new GetProductsQuery(), cancellationToken);

            if (result.IsFailure)
                return this.ToActionResult(result.Error!);

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

    }
}
