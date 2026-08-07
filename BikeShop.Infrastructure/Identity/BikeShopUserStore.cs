using BikeShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Identity
{
    public class BikeShopUserStore : UserStore<ApplicationUser, IdentityRole<int>, BikeShopDbContext, int>
    {
        public BikeShopUserStore(BikeShopDbContext context) : base(context)
        {
            AutoSaveChanges = false;
        }
    }
}
