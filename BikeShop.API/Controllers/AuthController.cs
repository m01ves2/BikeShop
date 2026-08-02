using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Authentication.Register;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _userService.RegisterAsync(command.Email, command.Password, cancellationToken);

            if (!result.IsSuccess) {
                return Conflict(result.Error);
            }

            return Ok();
        }
    }
}
