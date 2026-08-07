using BikeShop.Domain.Entities;
using BikeShop.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeShop.Infrastructure.Persistence.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                    .HasMaxLength(100);

            builder.Property(c => c.LastName)
                   .HasMaxLength(100);

            builder.Property(c => c.Phone)
                   .HasMaxLength(30);

            builder.Property(c => c.Address)
                   .HasMaxLength(300);

            builder.HasIndex(c => c.ApplicationUserId)
                   .IsUnique();

            builder.HasOne<ApplicationUser>()
                   .WithOne()
                   .HasForeignKey<Customer>(c => c.ApplicationUserId);
        }
    }
}
