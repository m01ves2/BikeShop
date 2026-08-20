using BikeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeShop.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.DeliveryAddress)
                .IsRequired().HasMaxLength(100);

            builder.Property(x => x.CustomerPhone)
                .IsRequired().HasMaxLength(20);

            //эта конфигурация добавлена в OrderItemConfiguration:
            //builder.HasMany(x => x.Items)
            //    .WithOne(x => x.Order)
            //    .HasForeignKey(x => x.OrderId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}