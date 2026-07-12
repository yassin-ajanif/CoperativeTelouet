using CoperativeTelouet.Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Client;

public class FactureClientConfiguration : IEntityTypeConfiguration<FactureClient>
{
    public void Configure(EntityTypeBuilder<FactureClient> builder)
    {
        builder.ToTable("FacturesClient");

        builder.HasIndex(f => f.Numero).IsUnique();

        builder.Property(f => f.RemiseGlobale).HasPrecision(18, 2);
        builder.Property(f => f.TotalTtc).HasPrecision(18, 2);

        builder.HasOne(f => f.Client)
            .WithMany(t => t.FacturesClient)
            .HasForeignKey(f => f.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.DevisClient)
            .WithMany(d => d.Factures)
            .HasForeignKey(f => f.DevisClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(f => f.Lignes)
            .WithOne(l => l.FactureClient)
            .HasForeignKey(l => l.FactureClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Paiements)
            .WithOne(p => p.FactureClient)
            .HasForeignKey(p => p.FactureClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class FactureClientLigneConfiguration : IEntityTypeConfiguration<FactureClientLigne>
{
    public void Configure(EntityTypeBuilder<FactureClientLigne> builder)
    {
        builder.ToTable("FactureClientLignes", t => t.HasCheckConstraint(
            "CK_FactureClientLignes_ProduitXorService",
            "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)"));

        builder.Property(l => l.Quantite).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.BonLivraisonClient)
            .WithMany(b => b.FactureLignes)
            .HasForeignKey(l => l.BonLivraisonClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.FactureClientLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.FactureClientLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class PaiementClientConfiguration : IEntityTypeConfiguration<PaiementClient>
{
    public void Configure(EntityTypeBuilder<PaiementClient> builder)
    {
        builder.ToTable("PaiementsClient", t => t.HasCheckConstraint(
            "CK_PaiementsClient_Mode",
            "Mode IN ('Credit','Cheque','Especes','TPE','Virement','Effet')"));

        builder.Property(p => p.Mode)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Montant).HasPrecision(18, 2);
    }
}
