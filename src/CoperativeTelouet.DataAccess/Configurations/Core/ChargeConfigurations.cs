using CoperativeTelouet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Core;

public class TypeChargeConfiguration : IEntityTypeConfiguration<TypeCharge>
{
    public void Configure(EntityTypeBuilder<TypeCharge> builder)
    {
        builder.ToTable("TypesCharge");

        builder.HasIndex(t => t.Nom).IsUnique();
    }
}

public class ChargeConfiguration : IEntityTypeConfiguration<Charge>
{
    public void Configure(EntityTypeBuilder<Charge> builder)
    {
        builder.ToTable("Charges");

        builder.Property(c => c.MontantTtc).HasPrecision(18, 2);

        builder.HasOne(c => c.TypeCharge)
            .WithMany(t => t.Charges)
            .HasForeignKey(c => c.TypeChargeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Fournisseur)
            .WithMany(t => t.Charges)
            .HasForeignKey(c => c.FournisseurId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
