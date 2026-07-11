using BikeShop.Domain.Entities;

namespace BikeShop.Application.Abstractions.Persistence
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Category category, CancellationToken cancellationToken);

        //Task<Category?> GetByIdAsync(...)
        //Task DeleteAsync(Category category,...)
    }
}
