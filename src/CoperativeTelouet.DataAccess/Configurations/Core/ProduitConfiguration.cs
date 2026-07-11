using CoperativeTelouet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Core;

public class ProduitConfiguration : IEntityTypeConfiguration<Produit>
{
    public void Configure(EntityTypeBuilder<Produit> builder)
    {
        builder.ToTable("Produits");

        builder.HasIndex(p => p.Reference).IsUnique();

        builder.Property(p => p.PrixAchatHT).HasPrecision(18, 2);
        builder.Property(p => p.PrixVenteHT).HasPrecision(18, 2);
        builder.Property(p => p.TauxTVA).HasPrecision(18, 2);
        builder.Property(p => p.StockActuel).HasPrecision(18, 3);
        builder.Property(p => p.StockMinimum).HasPrecision(18, 3);

        builder.HasOne(p => p.Categorie)
            .WithMany(c => c.Produits)
            .HasForeignKey(p => p.CategorieId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
