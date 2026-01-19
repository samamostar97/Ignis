using Ignis.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ignis.Infrastructure.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            // Unique constraint on Name
            builder.HasIndex(s => s.Name)
                .IsUnique();
        }
    }
}
