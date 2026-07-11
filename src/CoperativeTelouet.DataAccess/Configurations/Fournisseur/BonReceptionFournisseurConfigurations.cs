using CoperativeTelouet.Domain.Entities.Fournisseur;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Fournisseur;

public class BonReceptionFournisseurConfiguration : IEntityTypeConfiguration<BonReceptionFournisseur>
{
    public void Configure(EntityTypeBuilder<BonReceptionFournisseur> builder)
    {
        builder.ToTable("BonsReceptionFournisseur");

        builder.HasIndex(b => b.Numero).IsUnique();

        builder.Property(b => b.TotalTtc).HasPrecision(18, 2);

        builder.HasOne(b => b.BonCommandeFournisseur)
            .WithMany(c => c.BonsReception)
            .HasForeignKey(b => b.BonCommandeFournisseurId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Fournisseur)
            .WithMany(t => t.BonsReceptionFournisseur)
            .HasForeignKey(b => b.FournisseurId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.DevisFournisseur)
            .WithMany(d => d.BonsReception)
            .HasForeignKey(b => b.DevisFournisseurId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(b => b.FactureFournisseur)
            .WithMany(f => f.BonsReception)
            .HasForeignKey(b => b.FactureFournisseurId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(b => b.Lignes)
            .WithOne(l => l.BonReceptionFournisseur)
            .HasForeignKey(l => l.BonReceptionFournisseurId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class BonReceptionFournisseurLigneConfiguration : IEntityTypeConfiguration<BonReceptionFournisseurLigne>
{
    public void Configure(EntityTypeBuilder<BonReceptionFournisseurLigne> builder)
    {
        builder.ToTable("BonReceptionFournisseurLignes");

        builder.Property(l => l.QuantiteRecue).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.BonReceptionFournisseurLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.BonReceptionFournisseurLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
