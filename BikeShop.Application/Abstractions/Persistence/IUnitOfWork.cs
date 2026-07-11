namespace BikeShop.Application.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
