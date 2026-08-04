using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Authentication.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Authentication.Login
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, Result<LoginDto>>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<LoginDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.LoginAsync(command.Email, command.Password, command.rememberMe, cancellationToken);
        }
    }
}
