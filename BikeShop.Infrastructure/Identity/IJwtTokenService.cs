namespace BikeShop.Infrastructure.Identity
{
    public interface IJwtTokenService
    {
        string CreateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
