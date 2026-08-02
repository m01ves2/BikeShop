using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace BikeShop.Infrastructure.Identity
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<Result> RegisterAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);


            if (!result.Succeeded) {
                var errors = result.Errors.Select(x => x.Description);

                return Result.Failure( new Error( ErrorCode.Unexpected, string.Join("; ", errors)));
            }

            return Result.Success();
        }
    }
}
