using CoperativeTelouet.Domain.Entities.Stockage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Stockage;

public class BonSortieStockageConfiguration : IEntityTypeConfiguration<BonSortieStockage>
{
    public void Configure(EntityTypeBuilder<BonSortieStockage> builder)
    {
        builder.ToTable("BonsSortieStockage", t =>
        {
            t.HasCheckConstraint(
                "CK_BonsSortieStockage_EtatBac",
                "EtatBac IN ('Vide','Plein')");

            t.HasCheckConstraint(
                "CK_BonsSortieStockage_EtatBac_Fields",
                "(EtatBac = 'Vide' AND BonEntreeStockageId IS NULL AND FactureClientId IS NULL) " +
                "OR " +
                "(EtatBac = 'Plein' AND BonEntreeStockageId IS NOT NULL)");
        });

        builder.HasIndex(b => b.Numero).IsUnique();

        builder.Property(b => b.EtatBac)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.HasOne(b => b.Client)
            .WithMany(t => t.BonsSortieStockage)
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.BonEntreeStockage)
            .WithMany(e => e.Sorties)
            .HasForeignKey(b => b.BonEntreeStockageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(b => b.FactureClient)
            .WithMany(f => f.BonsSortieStockage)
            .HasForeignKey(b => b.FactureClientId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
