using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.CancelOrder;
using BikeShop.Application.Orders.CreateOrder;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Application.Orders.GetCustomerOrders;
using BikeShop.Application.Orders.GetOrderDetails;
using BikeShop.Application.Orders.Requests;
using BikeShop.Application.Orders.UpdateOrder;
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
        private readonly ICommandHandler<UpdateOrderCommand, Result> _updateOrderCommandHandler;
        private readonly ICommandHandler<CancelOrderCommand, Result> _cancelOrderCommandHandler;

        public OrdersController(ICommandHandler<CreateOrderCommand, Result<CreateOrderResultDto>> createOrderCommandHandler,
                                IQueryHandler<GetCustomerOrdersQuery, Result<IReadOnlyList<OrderListItemDto>>> getCustomerOrdersQueryHandler,
                                IQueryHandler<GetOrderDetailsQuery, Result<OrderDetailsDto>> getOrderDetailsQueryHandler,
                                ICommandHandler<UpdateOrderCommand, Result> updateOrderCommandHandler,
                                ICommandHandler<CancelOrderCommand, Result> cancelOrderCommandHandler)
        {
            _createOrderCommandHandler = createOrderCommandHandler;
            _getCustomerOrdersQueryHandler = getCustomerOrdersQueryHandler;
            _getOrderDetailsQueryHandler = getOrderDetailsQueryHandler;
            _updateOrderCommandHandler = updateOrderCommandHandler;
            _cancelOrderCommandHandler = cancelOrderCommandHandler;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();
            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;

            if (id != request.OrderId) {
                return this.ToActionResult(new Error(ErrorCode.Validation, "Route id does not match body id."));
            }

            var result = await _updateOrderCommandHandler.Handle(
                new UpdateOrderCommand(applicationUserId, request.OrderId, request.DeliveryAddress, request.DeliveryAt, request.CustomerPhone ),
                cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder([FromRoute] int id, CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();

            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;

            var result = await _cancelOrderCommandHandler.Handle( new CancelOrderCommand(applicationUserId, id), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok();
        }
    }
}
