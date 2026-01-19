using Ignis.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ignis.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Unique constraint on OrderNumber
            builder.HasIndex(o => o.OrderNumber)
                .IsUnique();

            // Decimal precision for money fields
            builder.Property(o => o.Subtotal)
                .HasPrecision(18, 2);

            builder.Property(o => o.Tax)
                .HasPrecision(18, 2);

            builder.Property(o => o.ShippingCost)
                .HasPrecision(18, 2);

            builder.Property(o => o.Total)
                .HasPrecision(18, 2);

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
