using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Domain.Entities;
using BikeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BikeShopDbContext _context;

        public ProductRepository(BikeShopDbContext context)
        {
            _context = context;
        }


        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.Include(p => p.Category).AsNoTracking().ToListAsync(cancellationToken); //тут только читаем, поэтому AsNoTracking. EF не должен отслеживать изменения.
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
