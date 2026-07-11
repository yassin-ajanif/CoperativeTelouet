using CoperativeTelouet.Domain.Entities.Stockage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Stockage;

public class BonEntreeStockageConfiguration : IEntityTypeConfiguration<BonEntreeStockage>
{
    public void Configure(EntityTypeBuilder<BonEntreeStockage> builder)
    {
        builder.ToTable("BonsEntreeStockage", t =>
        {
            t.HasCheckConstraint(
                "CK_BonsEntreeStockage_EtatBac",
                "EtatBac IN ('Vide','Plein')");

            t.HasCheckConstraint(
                "CK_BonsEntreeStockage_EtatBac_Fields",
                "(EtatBac = 'Vide' AND ChambreFroideId IS NULL AND VarieteId IS NULL " +
                "AND NumeroLot IS NULL AND PrixParBacParJourApplique IS NULL) " +
                "OR " +
                "(EtatBac = 'Plein' AND ChambreFroideId IS NOT NULL AND VarieteId IS NOT NULL " +
                "AND NumeroLot IS NOT NULL AND PrixParBacParJourApplique IS NOT NULL)");
        });

        builder.HasIndex(b => b.Numero).IsUnique();
        builder.HasIndex(b => b.NumeroLot).IsUnique();

        builder.Property(b => b.EtatBac)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(b => b.PrixParBacParJourApplique).HasPrecision(18, 2);

        builder.HasOne(b => b.Client)
            .WithMany(t => t.BonsEntreeStockage)
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.ChambreFroide)
            .WithMany(c => c.BonsEntree)
            .HasForeignKey(b => b.ChambreFroideId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Variete)
            .WithMany(v => v.BonsEntree)
            .HasForeignKey(b => b.VarieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
