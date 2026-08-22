using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Domain.Entities;
using BikeShop.Infrastructure.Persistence;

namespace BikeShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BikeShopDbContext _context;
        public OrderRepository(BikeShopDbContext context) {
            _context = context;
        }
        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order);
        }
    }
}
