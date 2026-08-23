using BikeShop.Domain.Entities;

namespace BikeShop.Application.Abstractions.Persistence
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);
        Task<Order?> GetByIdAsync(int customerId, int orderId, CancellationToken cancellationToken);
    }

}
