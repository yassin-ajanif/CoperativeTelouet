namespace CoperativeTelouet.UI.Services;

/// <summary>Root of column-visibility.json — one prefs object per document type.</summary>
public sealed class DocumentColumnVisibilityStore
{
    public DocumentColumnVisibility DevisClient { get; set; } = new();
    public DocumentColumnVisibility BonCommandeClient { get; set; } = new();
    public DocumentColumnVisibility BonLivraisonClient { get; set; } = new();
    public DocumentColumnVisibility FactureClient { get; set; } = new();
    public DocumentColumnVisibility AvoirClient { get; set; } = new();
    public DocumentColumnVisibility DevisFournisseur { get; set; } = new();
    public DocumentColumnVisibility BonCommandeFournisseur { get; set; } = new();
    public DocumentColumnVisibility BonReceptionFournisseur { get; set; } = new();
    public DocumentColumnVisibility FactureFournisseur { get; set; } = new();
    public DocumentColumnVisibility AvoirFournisseur { get; set; } = new();

    public DocumentColumnVisibility Get(string documentKey) => documentKey switch
    {
        DocumentColumnKeys.DevisClient => DevisClient,
        DocumentColumnKeys.BonCommandeClient => BonCommandeClient,
        DocumentColumnKeys.BonLivraisonClient => BonLivraisonClient,
        DocumentColumnKeys.FactureClient => FactureClient,
        DocumentColumnKeys.AvoirClient => AvoirClient,
        DocumentColumnKeys.DevisFournisseur => DevisFournisseur,
        DocumentColumnKeys.BonCommandeFournisseur => BonCommandeFournisseur,
        DocumentColumnKeys.BonReceptionFournisseur => BonReceptionFournisseur,
        DocumentColumnKeys.FactureFournisseur => FactureFournisseur,
        DocumentColumnKeys.AvoirFournisseur => AvoirFournisseur,
        _ => throw new ArgumentOutOfRangeException(nameof(documentKey), documentKey, null),
    };

    public void Set(string documentKey, DocumentColumnVisibility visibility)
    {
        ArgumentNullException.ThrowIfNull(visibility);
        switch (documentKey)
        {
            case DocumentColumnKeys.DevisClient: DevisClient = visibility; break;
            case DocumentColumnKeys.BonCommandeClient: BonCommandeClient = visibility; break;
            case DocumentColumnKeys.BonLivraisonClient: BonLivraisonClient = visibility; break;
            case DocumentColumnKeys.FactureClient: FactureClient = visibility; break;
            case DocumentColumnKeys.AvoirClient: AvoirClient = visibility; break;
            case DocumentColumnKeys.DevisFournisseur: DevisFournisseur = visibility; break;
            case DocumentColumnKeys.BonCommandeFournisseur: BonCommandeFournisseur = visibility; break;
            case DocumentColumnKeys.BonReceptionFournisseur: BonReceptionFournisseur = visibility; break;
            case DocumentColumnKeys.FactureFournisseur: FactureFournisseur = visibility; break;
            case DocumentColumnKeys.AvoirFournisseur: AvoirFournisseur = visibility; break;
            default: throw new ArgumentOutOfRangeException(nameof(documentKey), documentKey, null);
        }
    }
}

public static class DocumentColumnKeys
{
    public const string DevisClient = "devisClient";
    public const string BonCommandeClient = "bonCommandeClient";
    public const string BonLivraisonClient = "bonLivraisonClient";
    public const string FactureClient = "factureClient";
    public const string AvoirClient = "avoirClient";
    public const string DevisFournisseur = "devisFournisseur";
    public const string BonCommandeFournisseur = "bonCommandeFournisseur";
    public const string BonReceptionFournisseur = "bonReceptionFournisseur";
    public const string FactureFournisseur = "factureFournisseur";
    public const string AvoirFournisseur = "avoirFournisseur";
}
