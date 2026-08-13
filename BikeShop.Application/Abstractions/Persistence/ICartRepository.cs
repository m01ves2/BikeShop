using BikeShop.Domain.Entities;

namespace BikeShop.Application.Abstractions.Persistence
{
    public interface ICartRepository
    {
        Task AddAsync(Cart cart, CancellationToken cancellationToken);
    }
}