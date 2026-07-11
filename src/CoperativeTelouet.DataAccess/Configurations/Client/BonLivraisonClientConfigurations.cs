using CoperativeTelouet.Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Client;

public class BonLivraisonClientConfiguration : IEntityTypeConfiguration<BonLivraisonClient>
{
    public void Configure(EntityTypeBuilder<BonLivraisonClient> builder)
    {
        builder.ToTable("BonsLivraisonClient");

        builder.HasIndex(b => b.Numero).IsUnique();

        builder.HasOne(b => b.Client)
            .WithMany(t => t.BonsLivraisonClient)
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.DevisClient)
            .WithMany(d => d.BonsLivraison)
            .HasForeignKey(b => b.DevisClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(b => b.BonCommandeClient)
            .WithMany(c => c.BonsLivraison)
            .HasForeignKey(b => b.BonCommandeClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(b => b.FactureClient)
            .WithMany(f => f.BonsLivraison)
            .HasForeignKey(b => b.FactureClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(b => b.Lignes)
            .WithOne(l => l.BonLivraisonClient)
            .HasForeignKey(l => l.BonLivraisonClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class BonLivraisonClientLigneConfiguration : IEntityTypeConfiguration<BonLivraisonClientLigne>
{
    public void Configure(EntityTypeBuilder<BonLivraisonClientLigne> builder)
    {
        builder.ToTable("BonLivraisonClientLignes");

        builder.Property(l => l.QuantiteCommandee).HasPrecision(18, 3);
        builder.Property(l => l.QuantiteLivree).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.BonLivraisonClientLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.BonLivraisonClientLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
