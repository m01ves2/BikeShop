using BikeShop.Domain.Entities;

namespace BikeShop.Application.Abstractions.Persistence
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken);
        //Task<Category?> GetByIdAsync(...)
        //Task AddAsync(Category category,...)
        //Task DeleteAsync(Category category,...)
    }
}
