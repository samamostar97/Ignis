using Ignis.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ignis.Infrastructure.Data.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            // Unique constraint - one address per user
            builder.HasIndex(a => a.UserId)
                .IsUnique();
        }
    }
}
