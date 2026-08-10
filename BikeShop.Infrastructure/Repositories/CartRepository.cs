using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Domain.Entities;
using BikeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly BikeShopDbContext _context;

        public CartRepository(BikeShopDbContext context)
        {
            _context = context;
        }


        public async Task<Cart?> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
        {
            return await _context.Carts.Include(c => c.Items).
                                        ThenInclude(i => i.Product).
                                        FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);
        }

        public async Task AddAsync(Cart cart, CancellationToken cancellationToken)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
        }
    }
}
