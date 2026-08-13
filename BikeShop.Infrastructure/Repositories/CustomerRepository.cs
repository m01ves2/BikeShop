using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Domain.Entities;
using BikeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BikeShopDbContext _context;

        public CustomerRepository(BikeShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(customer, cancellationToken);
        }

        public async Task<Customer?> GetCustomerByApplicationUserIdAsync(int applicationUserId, CancellationToken cancellationToken)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId, cancellationToken);
        }

        public async Task<Customer?> GetCustomerWithCartByApplicationUserIdAsync(int applicationUserId, CancellationToken cancellationToken)
        {
            return await _context.Customers.Include(c => c.Cart).FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId, cancellationToken);
        }
    }
}
