using BikeShop.Domain.Entities;

namespace BikeShop.Application.Abstractions.Persistence
{
    public interface ICartRepository
    {
        Task<Cart?> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
        Task AddAsync(Cart cart, CancellationToken cancellationToken);
    }
}
