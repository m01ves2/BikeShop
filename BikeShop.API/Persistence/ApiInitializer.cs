using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Admin.Identity.Register;
using BikeShop.Application.Common.Models;
using BikeShop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Persistence
{
    public static class ApiInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminEmail = "admin@bikeshop.com";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin is null) {
                var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<RegisterAdminCommand, Result>>();

                var result = await handler.Handle(new RegisterAdminCommand(adminEmail, "Admin123!"), CancellationToken.None);

                if (result.IsFailure) {
                    throw new InvalidOperationException($"Failed to create admin: {result.Error!.Message}");
                }
            }
        }
    }
}
