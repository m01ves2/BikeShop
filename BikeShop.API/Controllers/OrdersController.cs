using System.Security.Claims;
using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Carts.AddItem;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.CreateOrder;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Application.Orders.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : BikeShopController
    {
        private readonly ICommandHandler<CreateOrderCommand, Result<CreateOrderResultDto>> _createOrderCommandHandler;

        public OrdersController(ICommandHandler<CreateOrderCommand, Result<CreateOrderResultDto>> createOrderCommandHandler)
        {
            _createOrderCommandHandler = createOrderCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request,  CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();
            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;
            var result = await _createOrderCommandHandler.Handle(
                new CreateOrderCommand(applicationUserId, request.DeliveryAddress, request.DeliveryAt, request.CustomerPhone), 
                cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok(result.Data);
        }
    }
}
