using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Domain.Entities;
using BikeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Order?> GetByIdForCustomerAsync(int customerId, int orderId,  CancellationToken cancellationToken)
        {
            return await _context.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == orderId && x.CustomerId == customerId, cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
        }
    }
}
