using CoperativeTelouet.Domain.Entities.Fournisseur;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Fournisseur;

public class AvoirFournisseurConfiguration : IEntityTypeConfiguration<AvoirFournisseur>
{
    public void Configure(EntityTypeBuilder<AvoirFournisseur> builder)
    {
        builder.ToTable("AvoirsFournisseur");

        builder.HasIndex(a => a.Numero).IsUnique();

        builder.Property(a => a.TotalTtc).HasPrecision(18, 2);

        builder.HasOne(a => a.FactureFournisseur)
            .WithMany(f => f.Avoirs)
            .HasForeignKey(a => a.FactureFournisseurId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Fournisseur)
            .WithMany(t => t.AvoirsFournisseur)
            .HasForeignKey(a => a.FournisseurId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Lignes)
            .WithOne(l => l.AvoirFournisseur)
            .HasForeignKey(l => l.AvoirFournisseurId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class AvoirFournisseurLigneConfiguration : IEntityTypeConfiguration<AvoirFournisseurLigne>
{
    public void Configure(EntityTypeBuilder<AvoirFournisseurLigne> builder)
    {
        builder.ToTable("AvoirFournisseurLignes");

        builder.Property(l => l.Quantite).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.AvoirFournisseurLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.AvoirFournisseurLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
