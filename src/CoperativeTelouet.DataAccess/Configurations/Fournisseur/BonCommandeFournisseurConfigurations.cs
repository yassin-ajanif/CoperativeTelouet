using CoperativeTelouet.Domain.Entities.Fournisseur;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Fournisseur;

public class BonCommandeFournisseurConfiguration : IEntityTypeConfiguration<BonCommandeFournisseur>
{
    public void Configure(EntityTypeBuilder<BonCommandeFournisseur> builder)
    {
        builder.ToTable("BonsCommandeFournisseur");

        builder.HasIndex(b => b.Numero).IsUnique();

        builder.HasOne(b => b.Fournisseur)
            .WithMany(t => t.BonsCommandeFournisseur)
            .HasForeignKey(b => b.FournisseurId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.DevisFournisseur)
            .WithMany(d => d.BonsCommande)
            .HasForeignKey(b => b.DevisFournisseurId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(b => b.FactureFournisseur)
            .WithMany(f => f.BonsCommande)
            .HasForeignKey(b => b.FactureFournisseurId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(b => b.Lignes)
            .WithOne(l => l.BonCommandeFournisseur)
            .HasForeignKey(l => l.BonCommandeFournisseurId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class BonCommandeFournisseurLigneConfiguration : IEntityTypeConfiguration<BonCommandeFournisseurLigne>
{
    public void Configure(EntityTypeBuilder<BonCommandeFournisseurLigne> builder)
    {
        builder.ToTable("BonCommandeFournisseurLignes");

        builder.Property(l => l.QuantiteCommandee).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.BonCommandeFournisseurLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.BonCommandeFournisseurLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
