using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Authentication.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Authentication.Login
{
    public sealed record LoginUserCommand(string Email, bool rememberMe, string Password) : ICommand<Result<LoginDto>>;
}
