using CoperativeTelouet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Core;

public class TiersConfiguration : IEntityTypeConfiguration<Tiers>
{
    public void Configure(EntityTypeBuilder<Tiers> builder)
    {
        builder.ToTable("Tiers", t => t.HasCheckConstraint(
            "CK_Tiers_Type",
            "Type IN ('Client','Fournisseur','LesDeux')"));

        builder.Property(t => t.Type)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
    }
}
