using CoperativeTelouet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Core;

public class ServiceItemConfiguration : IEntityTypeConfiguration<ServiceItem>
{
    public void Configure(EntityTypeBuilder<ServiceItem> builder)
    {
        builder.ToTable("Services");

        builder.HasIndex(s => s.Reference).IsUnique();

        builder.Property(s => s.PrixVenteHT).HasPrecision(18, 2);
        builder.Property(s => s.CoutHT).HasPrecision(18, 2);
        builder.Property(s => s.TauxTVA).HasPrecision(18, 2);
    }
}
