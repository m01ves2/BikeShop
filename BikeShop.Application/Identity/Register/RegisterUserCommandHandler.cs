using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Authentication.Register
{
    public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.RegisterAsync(command.Email, command.Password, cancellationToken);
        }
    }
}
