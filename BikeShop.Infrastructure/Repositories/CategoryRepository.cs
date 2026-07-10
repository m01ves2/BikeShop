using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Domain.Entities;
using BikeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BikeShopDbContext _context;

        public CategoryRepository(BikeShopDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync( CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().ToListAsync(cancellationToken); //тут только читаем, поэтому AsNoTracking. EF не должен отслеживать изменения.
            //Для Query: 
            //    GetCategories, GetProducts, GetOrdersHistory
        }
    }
}
