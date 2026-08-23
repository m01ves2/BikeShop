using System.Security.Claims;
using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Carts.AddItem;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.CreateOrder;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Application.Orders.GetCustomerOrders;
using BikeShop.Application.Orders.GetOrderDetails;
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
        private readonly IQueryHandler<GetCustomerOrdersQuery, Result<IReadOnlyList<OrderListItemDto>>> _getCustomerOrdersQueryHandler;
        private readonly IQueryHandler<GetOrderDetailsQuery, Result<OrderDetailsDto>> _getOrderDetailsQueryHandler;

        public OrdersController(ICommandHandler<CreateOrderCommand, Result<CreateOrderResultDto>> createOrderCommandHandler,
                                IQueryHandler<GetCustomerOrdersQuery, Result<IReadOnlyList<OrderListItemDto>>> getCustomerOrdersQueryHandler,
                                IQueryHandler<GetOrderDetailsQuery, Result<OrderDetailsDto>> getOrderDetailsQueryHandler)
        {
            _createOrderCommandHandler = createOrderCommandHandler;
            _getCustomerOrdersQueryHandler = getCustomerOrdersQueryHandler;
            _getOrderDetailsQueryHandler = getOrderDetailsQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();
            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;
            var result = await _getCustomerOrdersQueryHandler.Handle(new GetCustomerOrdersQuery(applicationUserId), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();
            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;

            var result = await _getOrderDetailsQueryHandler.Handle(new GetOrderDetailsQuery(applicationUserId, id), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok(result.Data);
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
