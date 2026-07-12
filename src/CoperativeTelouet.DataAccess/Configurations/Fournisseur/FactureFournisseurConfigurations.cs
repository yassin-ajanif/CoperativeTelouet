using CoperativeTelouet.Domain.Entities.Fournisseur;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Fournisseur;

public class FactureFournisseurConfiguration : IEntityTypeConfiguration<FactureFournisseur>
{
    public void Configure(EntityTypeBuilder<FactureFournisseur> builder)
    {
        builder.ToTable("FacturesFournisseur");

        builder.HasIndex(f => f.Numero).IsUnique();

        builder.Property(f => f.RemiseGlobale).HasPrecision(18, 2);
        builder.Property(f => f.TotalTtc).HasPrecision(18, 2);

        builder.HasOne(f => f.Fournisseur)
            .WithMany(t => t.FacturesFournisseur)
            .HasForeignKey(f => f.FournisseurId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.DevisFournisseur)
            .WithMany(d => d.Factures)
            .HasForeignKey(f => f.DevisFournisseurId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(f => f.Lignes)
            .WithOne(l => l.FactureFournisseur)
            .HasForeignKey(l => l.FactureFournisseurId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Paiements)
            .WithOne(p => p.FactureFournisseur)
            .HasForeignKey(p => p.FactureFournisseurId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class FactureFournisseurLigneConfiguration : IEntityTypeConfiguration<FactureFournisseurLigne>
{
    public void Configure(EntityTypeBuilder<FactureFournisseurLigne> builder)
    {
        builder.ToTable("FactureFournisseurLignes", t => t.HasCheckConstraint(
            "CK_FactureFournisseurLignes_ProduitXorService",
            "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)"));

        builder.Property(l => l.Quantite).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.BonReceptionFournisseur)
            .WithMany(b => b.FactureLignes)
            .HasForeignKey(l => l.BonReceptionFournisseurId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.FactureFournisseurLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.FactureFournisseurLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class PaiementFournisseurConfiguration : IEntityTypeConfiguration<PaiementFournisseur>
{
    public void Configure(EntityTypeBuilder<PaiementFournisseur> builder)
    {
        builder.ToTable("PaiementsFournisseur", t => t.HasCheckConstraint(
            "CK_PaiementsFournisseur_Mode",
            "Mode IN ('Credit','Cheque','Especes','TPE','Virement','Effet')"));

        builder.Property(p => p.Mode)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Montant).HasPrecision(18, 2);
    }
}
