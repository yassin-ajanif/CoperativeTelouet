using CoperativeTelouet.Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Client;

public class AvoirClientConfiguration : IEntityTypeConfiguration<AvoirClient>
{
    public void Configure(EntityTypeBuilder<AvoirClient> builder)
    {
        builder.ToTable("AvoirsClient");

        builder.HasIndex(a => a.Numero).IsUnique();

        builder.HasOne(a => a.FactureClient)
            .WithMany(f => f.Avoirs)
            .HasForeignKey(a => a.FactureClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Client)
            .WithMany(t => t.AvoirsClient)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Lignes)
            .WithOne(l => l.AvoirClient)
            .HasForeignKey(l => l.AvoirClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class AvoirClientLigneConfiguration : IEntityTypeConfiguration<AvoirClientLigne>
{
    public void Configure(EntityTypeBuilder<AvoirClientLigne> builder)
    {
        builder.ToTable("AvoirClientLignes");

        builder.Property(l => l.Quantite).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.AvoirClientLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.AvoirClientLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
