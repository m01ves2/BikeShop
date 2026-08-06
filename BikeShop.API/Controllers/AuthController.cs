using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Authentication.DTOs;
using BikeShop.Application.Authentication.Login;
using BikeShop.Application.Authentication.Register;
using BikeShop.Application.Categories.CreateCategory;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandHandler<RegisterUserCommand, Result> _registerUserCommandHandler;
        private readonly ICommandHandler<LoginUserCommand, Result<LoginDto>> _loginUserCommandHandler;

        public AuthController(  ICommandHandler<RegisterUserCommand, Result> registerUserCommandHandler, 
                                ICommandHandler<LoginUserCommand, Result<LoginDto>> loginUserCommandHandler)
        {
            _registerUserCommandHandler = registerUserCommandHandler;
            _loginUserCommandHandler = loginUserCommandHandler;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _registerUserCommandHandler.Handle(command, cancellationToken);

            if (!result.IsSuccess) {
                return Conflict(result.Error?.Message ?? "Unknown error");
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _loginUserCommandHandler.Handle(command, cancellationToken);

            if (!result.IsSuccess) {
                return Conflict(result.Error?.Message ?? "Unknown error");
            }

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                Email = User.Identity?.Name
            });
        }
    }
}
