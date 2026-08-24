using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Admin.DTOs;
using BikeShop.Application.Admin.Orders.ChangeOrderStatus;
using BikeShop.Application.Admin.Orders.GetOrderDetails;
using BikeShop.Application.Admin.Requests;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/orders")]
    public class AdminOrdersController : ControllerBase
    {
        private readonly IQueryHandler<AdminGetOrderDetailsQuery, Result<AdminOrderDetailsDto>> _getOrderDetailsQueryHandler;
        private readonly ICommandHandler<AdminChangeOrderStatusCommand, Result> _changeOrderStatusCommandHandler;

        public AdminOrdersController(   IQueryHandler<AdminGetOrderDetailsQuery, Result<AdminOrderDetailsDto>> getOrderDetailsQueryHandler,
                                        ICommandHandler<AdminChangeOrderStatusCommand, Result> changeOrderStatusCommandHandler)
        {
            _getOrderDetailsQueryHandler = getOrderDetailsQueryHandler;
            _changeOrderStatusCommandHandler = changeOrderStatusCommandHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _getOrderDetailsQueryHandler.Handle( new AdminGetOrderDetailsQuery(id), cancellationToken);

            if (result.IsFailure)
                return this.ToActionResult(result.Error!);

            return Ok(result.Data);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] int id, ChangeOrderStatusRequest request, CancellationToken cancellationToken)
        {
            if (id != request.OrderId) {
                return this.ToActionResult(new Error(ErrorCode.Validation, "Route id does not match body id."));
            }

            var result = await _changeOrderStatusCommandHandler.Handle(
                new AdminChangeOrderStatusCommand(request.OrderId, request.OrderStatusDto), 
                cancellationToken );

            if (result.IsFailure)
                return this.ToActionResult(result.Error!);

            return Ok();
        }

        // TODO: Add admin endpoint for viewing all orders of a specific customer. Usecase ready!
        // Example route: GET /api/admin/customers/{customerId}/orders
    }
}