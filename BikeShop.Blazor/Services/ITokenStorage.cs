namespace BikeShop.Blazor.Services
{
    public interface ITokenStorage
    {
        Task SaveTokenAsync(string token);
        Task<string?> GetTokenAsync();
        Task RemoveTokenAsync();
    }
}
