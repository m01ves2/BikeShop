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

        public async Task AddAsync(Cart cart, CancellationToken cancellationToken)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
        }
    }
}
