using BikeShop.Domain.Entities;

namespace BikeShop.Application.Abstractions.Persistence
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer, CancellationToken cancellationToken);
        Task<Customer?> GetCustomerByApplicationUserIdAsync(int applicationUserId, CancellationToken cancellationToken);
        Task<Customer?> GetCustomerWithCartByApplicationUserIdAsync(int applicationUserId, CancellationToken cancellationToken);
        Task<Customer?> GetCustomerWithOrdersByApplicationUserIdAsync(int applicationUserId, CancellationToken cancellationToken);
    }
}
