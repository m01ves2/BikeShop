using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Carts.GetCartByCustomerId;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IQueryHandler<GetCartByCustomerIdQuery, Result<CartDto>> _getCartByCustomerIdQueryHandler;

        public CartsController(IQueryHandler<GetCartByCustomerIdQuery, Result<CartDto>> getCartByCustomerIdQueryHandler)
        {
            _getCartByCustomerIdQueryHandler = getCartByCustomerIdQueryHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int customerId, CancellationToken cancellationToken)
        {
            var result = await _getCartByCustomerIdQueryHandler.Handle(new GetCartByCustomerIdQuery(customerId), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }


            return Ok(result.Data);
        }
    }
}
