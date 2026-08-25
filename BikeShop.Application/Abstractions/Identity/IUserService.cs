using BikeShop.Application.Authentication.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Abstractions.Identity
{
    public interface IUserService
    {
        Task<Result<int>> RegisterAsync(string email, string password, IEnumerable<string> roles, CancellationToken cancellationToken);

        //Task<Result> LoginAsync(string email, string password, bool rememberMe, CancellationToken cancellationToken);
        Task<Result<LoginDto>> LoginAsync(string email, string password, bool rememberMe, CancellationToken cancellationToken);
    }
}
