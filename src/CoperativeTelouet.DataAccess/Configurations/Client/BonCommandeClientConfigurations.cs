using CoperativeTelouet.Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Client;

public class BonCommandeClientConfiguration : IEntityTypeConfiguration<BonCommandeClient>
{
    public void Configure(EntityTypeBuilder<BonCommandeClient> builder)
    {
        builder.ToTable("BonsCommandeClient");

        builder.HasIndex(b => b.Numero).IsUnique();

        builder.HasOne(b => b.Client)
            .WithMany(t => t.BonsCommandeClient)
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.DevisClient)
            .WithMany(d => d.BonsCommande)
            .HasForeignKey(b => b.DevisClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(b => b.FactureClient)
            .WithMany(f => f.BonsCommande)
            .HasForeignKey(b => b.FactureClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(b => b.Lignes)
            .WithOne(l => l.BonCommandeClient)
            .HasForeignKey(l => l.BonCommandeClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class BonCommandeClientLigneConfiguration : IEntityTypeConfiguration<BonCommandeClientLigne>
{
    public void Configure(EntityTypeBuilder<BonCommandeClientLigne> builder)
    {
        builder.ToTable("BonCommandeClientLignes");

        builder.Property(l => l.QuantiteCommandee).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.BonCommandeClientLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.BonCommandeClientLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
