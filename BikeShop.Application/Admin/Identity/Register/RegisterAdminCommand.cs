using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Admin.Identity.Register
{
    public sealed record RegisterAdminCommand(string Email, string Password) : ICommand<Result>;
}
