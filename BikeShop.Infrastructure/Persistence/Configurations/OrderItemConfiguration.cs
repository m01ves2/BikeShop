using BikeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeShop.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasOne(x => x.Product)
                    .WithMany()
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Order)
                    .WithMany(x => x.Items)
                    .HasForeignKey(x => x.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
