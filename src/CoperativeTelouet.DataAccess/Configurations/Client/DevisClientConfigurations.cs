using CoperativeTelouet.Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Client;

public class DevisClientConfiguration : IEntityTypeConfiguration<DevisClient>
{
    public void Configure(EntityTypeBuilder<DevisClient> builder)
    {
        builder.ToTable("DevisClient");

        builder.HasIndex(d => d.Numero).IsUnique();

        builder.Property(d => d.RemiseGlobale).HasPrecision(18, 2);
        builder.Property(d => d.TotalTtc).HasPrecision(18, 2);

        builder.HasOne(d => d.Client)
            .WithMany(t => t.DevisClients)
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Lignes)
            .WithOne(l => l.DevisClient)
            .HasForeignKey(l => l.DevisClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Conditions)
            .WithOne(c => c.DevisClient)
            .HasForeignKey(c => c.DevisClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class DevisClientLigneConfiguration : IEntityTypeConfiguration<DevisClientLigne>
{
    public void Configure(EntityTypeBuilder<DevisClientLigne> builder)
    {
        builder.ToTable("DevisClientLignes", t => t.HasCheckConstraint(
            "CK_DevisClientLignes_ProduitXorService",
            "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)"));

        builder.Property(l => l.Quantite).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.DevisClientLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.DevisClientLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class DevisClientConditionConfiguration : IEntityTypeConfiguration<DevisClientCondition>
{
    public void Configure(EntityTypeBuilder<DevisClientCondition> builder)
    {
        builder.ToTable("DevisClientConditions");
    }
}
