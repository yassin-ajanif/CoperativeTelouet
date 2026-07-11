using CoperativeTelouet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Core;

public class MouvementStockConfiguration : IEntityTypeConfiguration<MouvementStock>
{
    public void Configure(EntityTypeBuilder<MouvementStock> builder)
    {
        builder.ToTable("MouvementsStock", t => t.HasCheckConstraint(
            "CK_MouvementsStock_Type",
            "Type IN ('Entree','Sortie','Ajustement')"));

        builder.Property(m => m.Type)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(m => m.StockAvant).HasPrecision(18, 3);
        builder.Property(m => m.Quantite).HasPrecision(18, 3);

        builder.HasOne(m => m.Produit)
            .WithMany(p => p.Mouvements)
            .HasForeignKey(m => m.ProduitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
