using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Authentication.DTOs;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace BikeShop.Infrastructure.Identity
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        public IdentityUserService(UserManager<ApplicationUser> userManager, /*SignInManager<ApplicationUser> signInManager,*/ IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<Result<int>> RegisterAsync(string email, string password, IEnumerable<string> roles, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) {
                var errors = result.Errors.Select(x => x.Description);

                return Result<int>.Failure(
                    new Error(ErrorCode.Unexpected, string.Join("; ", errors)));
            }

            foreach (var role in roles) {
                var roleResult = await _userManager.AddToRoleAsync(user, role);

                if (!roleResult.Succeeded) {
                    var errors = roleResult.Errors.Select(x => x.Description);

                    return Result<int>.Failure(
                        new Error(ErrorCode.Unexpected, string.Join("; ", errors)));
                }
            }

            return Result<int>.Success(user.Id);
        }

        //Cookie-auth
        //public async Task<Result> LoginAsync(string email, string password, bool rememberMe, CancellationToken cancellationToken)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

        //    if (!result.Succeeded) {
        //        return Result.Failure(new Error(ErrorCode.Unexpected, "Invalid email or password"));
        //    }

        //    return Result.Success();
        //}

        //JWT-auth, add Set-Cookie to HTTP response.
        public async Task<Result<LoginDto>> LoginAsync(string email, string password,  bool rememberMe, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) {
                return Result<LoginDto>.Failure(new Error(ErrorCode.Unauthorized, "Invalid email or password"));
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!passwordValid) {
                return Result<LoginDto>.Failure(new Error(ErrorCode.Unauthorized, "Invalid email or password"));
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.CreateToken(user, roles);

            return Result<LoginDto>.Success(new LoginDto() { Token = token });
        }

    }
}
