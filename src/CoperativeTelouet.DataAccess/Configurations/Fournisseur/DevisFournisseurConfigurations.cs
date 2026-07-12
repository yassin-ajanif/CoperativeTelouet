using CoperativeTelouet.Domain.Entities.Fournisseur;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Fournisseur;

public class DevisFournisseurConfiguration : IEntityTypeConfiguration<DevisFournisseur>
{
    public void Configure(EntityTypeBuilder<DevisFournisseur> builder)
    {
        builder.ToTable("DevisFournisseur");

        builder.HasIndex(d => d.Numero).IsUnique();

        builder.Property(d => d.RemiseGlobale).HasPrecision(18, 2);
        builder.Property(d => d.TotalTtc).HasPrecision(18, 2);

        builder.HasOne(d => d.Fournisseur)
            .WithMany(t => t.DevisFournisseur)
            .HasForeignKey(d => d.FournisseurId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Lignes)
            .WithOne(l => l.DevisFournisseur)
            .HasForeignKey(l => l.DevisFournisseurId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Conditions)
            .WithOne(c => c.DevisFournisseur)
            .HasForeignKey(c => c.DevisFournisseurId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class DevisFournisseurLigneConfiguration : IEntityTypeConfiguration<DevisFournisseurLigne>
{
    public void Configure(EntityTypeBuilder<DevisFournisseurLigne> builder)
    {
        builder.ToTable("DevisFournisseurLignes");

        builder.Property(l => l.Quantite).HasPrecision(18, 3);
        builder.Property(l => l.PrixUnitaireHT).HasPrecision(18, 2);
        builder.Property(l => l.Remise).HasPrecision(18, 2);
        builder.Property(l => l.TauxTVA).HasPrecision(18, 2);

        builder.HasOne(l => l.Produit)
            .WithMany(p => p.DevisFournisseurLignes)
            .HasForeignKey(l => l.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Service)
            .WithMany(s => s.DevisFournisseurLignes)
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class DevisFournisseurConditionConfiguration : IEntityTypeConfiguration<DevisFournisseurCondition>
{
    public void Configure(EntityTypeBuilder<DevisFournisseurCondition> builder)
    {
        builder.ToTable("DevisFournisseurConditions");
    }
}
