using Ignis.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ignis.Infrastructure.Data.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            // Unique constraint on Code
            builder.HasIndex(c => c.Code)
                .IsUnique();

            // Decimal precision for money fields
            builder.Property(c => c.DiscountValue)
                .HasPrecision(18, 2);

            builder.Property(c => c.MinimumOrderAmount)
                .HasPrecision(18, 2);
        }
    }
}
