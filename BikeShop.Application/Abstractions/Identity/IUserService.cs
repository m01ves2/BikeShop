using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Abstractions.Identity
{
    public interface IUserService
    {
        Task<Result> RegisterAsync(string email, string password, CancellationToken cancellationToken);
    }
}
