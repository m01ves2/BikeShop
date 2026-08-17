using System.Security.Claims;
using BikeShop.API.Mappers;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Carts.AddItem;
using BikeShop.Application.Carts.ClearCart;
using BikeShop.Application.Carts.DecreaseItemQuantity;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Carts.GetCartByCustomerId;
using BikeShop.Application.Carts.IncreaseItemQuantity;
using BikeShop.Application.Carts.SynchronizeCart;
using BikeShop.Application.Carts.RemoveItem;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    //в API у запроса есть ещё одна очень важная вещь — Authorization header
    //Authorization: Bearer eyJhbGciOi...
    //тут вешаем на весь контроллер, на все методы
    [Authorize]
    [ApiController]
    [Route("api/cart")]
    public class CartsController : ControllerBase
    {
        private readonly IQueryHandler<GetCartQuery, Result<CartDto>> _getCartQueryHandler;
        private readonly ICommandHandler<AddItemCommand, Result> _addItemCommandHandler;
        private readonly ICommandHandler<RemoveItemCommand, Result> _removeItemCommandHandler;
        private readonly ICommandHandler<ClearCartCommand, Result> _clearCartCommandHandler;
        private readonly ICommandHandler<IncreaseItemQuantityCommand, Result> _increaseItemQuantityCommandHandler;
        private readonly ICommandHandler<DecreaseItemQuantityCommand, Result> _decreaseItemQuantityCommandHandler;
        private readonly ICommandHandler<SynchronizeCartCommand, Result<SynchronizeCartResultDto>> _synchrozineCartCommandHandler;

        public CartsController(IQueryHandler<GetCartQuery, Result<CartDto>> getCartQueryHandler,
                               ICommandHandler<AddItemCommand, Result> addItemCommandHandler,
                               ICommandHandler<RemoveItemCommand, Result> removeItemCommandHandler,
                               ICommandHandler<ClearCartCommand, Result> clearCartCommandHandler,
                               ICommandHandler<IncreaseItemQuantityCommand, Result> increaseItemQuantityCommandHandler,
                               ICommandHandler<DecreaseItemQuantityCommand, Result> decreaseItemQuantityCommandHandler,
                               ICommandHandler<SynchronizeCartCommand, Result<SynchronizeCartResultDto>> synchrozineCartCommandHandler)
        {
            _getCartQueryHandler = getCartQueryHandler;
            _addItemCommandHandler = addItemCommandHandler;
            _removeItemCommandHandler = removeItemCommandHandler;
            _clearCartCommandHandler = clearCartCommandHandler;
            _increaseItemQuantityCommandHandler = increaseItemQuantityCommandHandler;
            _decreaseItemQuantityCommandHandler = decreaseItemQuantityCommandHandler;
            _synchrozineCartCommandHandler = synchrozineCartCommandHandler;
        }


        [HttpGet]
        public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
        {

            var userIdResult = GetApplicationUserId();

            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);
            
            var applicationUserId = userIdResult.Data;
            var result = await _getCartQueryHandler.Handle(new GetCartQuery(applicationUserId), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok(result.Data);
        }


        [HttpPost("{productId}")]
        public async Task<IActionResult> AddCartItem(int productId, CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();

            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;
            var result = await _addItemCommandHandler.Handle(new AddItemCommand(applicationUserId, productId), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok();
        }


        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveCartItem(int productId, CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();

            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;
            var result = await _removeItemCommandHandler.Handle(new RemoveItemCommand(applicationUserId, productId), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();

            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;
            var result = await _clearCartCommandHandler.Handle(new ClearCartCommand(applicationUserId), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok();
        }

        [HttpPatch("{productId}/increase")]
        public async Task<IActionResult> IncreaseItemQuantity(int productId, int amount, CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();

            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;
            var result = await _increaseItemQuantityCommandHandler.Handle(new IncreaseItemQuantityCommand(applicationUserId, productId, amount), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok();
        }

        [HttpPatch("{productId}/decrease")]
        public async Task<IActionResult> DecreaseItemQuantity(int productId, int amount, CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();

            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;
            var result = await _decreaseItemQuantityCommandHandler.Handle(new DecreaseItemQuantityCommand(applicationUserId, productId, amount), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok();
        }

        [HttpPost("synchronize")]
        public async Task<IActionResult> SynchrozineCart([FromBody] CartDto localCart, CancellationToken cancellationToken)
        {
            var userIdResult = GetApplicationUserId();

            if (userIdResult.IsFailure)
                return this.ToActionResult(userIdResult.Error!);

            var applicationUserId = userIdResult.Data;
            var result = await _synchrozineCartCommandHandler.Handle(new SynchronizeCartCommand(applicationUserId, localCart), cancellationToken);

            if (result.IsFailure) {
                return this.ToActionResult(result.Error!);
            }

            return Ok(result.Data);
        }

        private Result<int> GetApplicationUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out var applicationUserId))
                return Result<int>.Failure(new Error(ErrorCode.Validation, "Invalid user identity"));

            return Result<int>.Success(applicationUserId);
        }
    }
}
