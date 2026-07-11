using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Entities.Client;
using CoperativeTelouet.Domain.Entities.Fournisseur;
using CoperativeTelouet.Domain.Entities.Stockage;
using Microsoft.EntityFrameworkCore;

namespace CoperativeTelouet.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Core
    public DbSet<Tiers> Tiers => Set<Tiers>();
    public DbSet<Categorie> Categories => Set<Categorie>();
    public DbSet<Produit> Produits => Set<Produit>();
    public DbSet<ServiceItem> Services => Set<ServiceItem>();
    public DbSet<MouvementStock> MouvementsStock => Set<MouvementStock>();
    public DbSet<TypeCharge> TypesCharge => Set<TypeCharge>();
    public DbSet<Charge> Charges => Set<Charge>();
    public DbSet<AppSettings> AppSettings => Set<AppSettings>();

    // Client
    public DbSet<DevisClient> DevisClients => Set<DevisClient>();
    public DbSet<DevisClientLigne> DevisClientLignes => Set<DevisClientLigne>();
    public DbSet<DevisClientCondition> DevisClientConditions => Set<DevisClientCondition>();
    public DbSet<BonCommandeClient> BonsCommandeClient => Set<BonCommandeClient>();
    public DbSet<BonCommandeClientLigne> BonCommandeClientLignes => Set<BonCommandeClientLigne>();
    public DbSet<BonLivraisonClient> BonsLivraisonClient => Set<BonLivraisonClient>();
    public DbSet<BonLivraisonClientLigne> BonLivraisonClientLignes => Set<BonLivraisonClientLigne>();
    public DbSet<FactureClient> FacturesClient => Set<FactureClient>();
    public DbSet<FactureClientLigne> FactureClientLignes => Set<FactureClientLigne>();
    public DbSet<PaiementClient> PaiementsClient => Set<PaiementClient>();
    public DbSet<AvoirClient> AvoirsClient => Set<AvoirClient>();
    public DbSet<AvoirClientLigne> AvoirClientLignes => Set<AvoirClientLigne>();

    // Fournisseur
    public DbSet<DevisFournisseur> DevisFournisseurs => Set<DevisFournisseur>();
    public DbSet<DevisFournisseurLigne> DevisFournisseurLignes => Set<DevisFournisseurLigne>();
    public DbSet<DevisFournisseurCondition> DevisFournisseurConditions => Set<DevisFournisseurCondition>();
    public DbSet<BonCommandeFournisseur> BonsCommandeFournisseur => Set<BonCommandeFournisseur>();
    public DbSet<BonCommandeFournisseurLigne> BonCommandeFournisseurLignes => Set<BonCommandeFournisseurLigne>();
    public DbSet<BonReceptionFournisseur> BonsReceptionFournisseur => Set<BonReceptionFournisseur>();
    public DbSet<BonReceptionFournisseurLigne> BonReceptionFournisseurLignes => Set<BonReceptionFournisseurLigne>();
    public DbSet<FactureFournisseur> FacturesFournisseur => Set<FactureFournisseur>();
    public DbSet<FactureFournisseurLigne> FactureFournisseurLignes => Set<FactureFournisseurLigne>();
    public DbSet<PaiementFournisseur> PaiementsFournisseur => Set<PaiementFournisseur>();
    public DbSet<AvoirFournisseur> AvoirsFournisseur => Set<AvoirFournisseur>();
    public DbSet<AvoirFournisseurLigne> AvoirFournisseurLignes => Set<AvoirFournisseurLigne>();

    // Stockage
    public DbSet<ChambreFroide> ChambresFroides => Set<ChambreFroide>();
    public DbSet<VarietePomme> VarietesPomme => Set<VarietePomme>();
    public DbSet<StockBacsSociete> StockBacsSociete => Set<StockBacsSociete>();
    public DbSet<BonEntreeStockage> BonsEntreeStockage => Set<BonEntreeStockage>();
    public DbSet<BonSortieStockage> BonsSortieStockage => Set<BonSortieStockage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyAuditInfo();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void ApplyAuditInfo()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}
