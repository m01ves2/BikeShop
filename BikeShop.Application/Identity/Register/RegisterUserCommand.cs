using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Authentication.Register
{

    public sealed record RegisterUserCommand(string Email, string Password) : ICommand<Result>;
}
