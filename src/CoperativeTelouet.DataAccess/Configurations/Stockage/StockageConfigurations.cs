using CoperativeTelouet.Domain.Entities.Stockage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoperativeTelouet.DataAccess.Configurations.Stockage;

public class ChambreFroideConfiguration : IEntityTypeConfiguration<ChambreFroide>
{
    public void Configure(EntityTypeBuilder<ChambreFroide> builder)
    {
        builder.ToTable("ChambresFroides");
    }
}

public class VarietePommeConfiguration : IEntityTypeConfiguration<VarietePomme>
{
    public void Configure(EntityTypeBuilder<VarietePomme> builder)
    {
        builder.ToTable("VarietesPomme");
    }
}

public class StockBacsSocieteConfiguration : IEntityTypeConfiguration<StockBacsSociete>
{
    public void Configure(EntityTypeBuilder<StockBacsSociete> builder)
    {
        builder.ToTable("StockBacsSociete");
    }
}
