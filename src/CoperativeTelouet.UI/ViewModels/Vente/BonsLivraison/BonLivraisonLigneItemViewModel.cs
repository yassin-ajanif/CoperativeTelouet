using CommunityToolkit.Mvvm.ComponentModel;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.Services;

namespace CoperativeTelouet.UI.ViewModels.Vente.BonsLivraison;

public partial class BonLivraisonLigneItemViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MontantHt))]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private int? _produitId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MontantHt))]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private int? _serviceId;

    [ObservableProperty]
    private string _designation = string.Empty;

    [ObservableProperty]
    private decimal _quantiteCommandee = 1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MontantHt))]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private decimal _quantite = 1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MontantHt))]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private decimal _prixUnitaireHt;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MontantHt))]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private decimal _remise;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private decimal _tauxTva;

    public decimal MontantHt => DocumentTotals.LineHt(Quantite, PrixUnitaireHt, Remise);
    public decimal MontantTtc => DocumentTotals.LineTtc(Quantite, PrixUnitaireHt, Remise, TauxTva);

    public CreateBonLivraisonClientLigneDto ToCreateDto() => new(
        ProduitId,
        ServiceId,
        Designation.Trim(),
        QuantiteCommandee,
        Quantite,
        PrixUnitaireHt,
        Remise,
        TauxTva);

    public static BonLivraisonLigneItemViewModel FromDto(BonLivraisonClientLigneDto dto) => new()
    {
        ProduitId = dto.ProduitId,
        ServiceId = dto.ServiceId,
        Designation = dto.Designation,
        QuantiteCommandee = dto.QuantiteCommandee,
        Quantite = dto.QuantiteLivree,
        PrixUnitaireHt = dto.PrixUnitaireHT,
        Remise = dto.Remise,
        TauxTva = dto.TauxTVA,
    };

    public static BonLivraisonLigneItemViewModel FromArticle(ArticleSuggestionDto article) => new()
    {
        ProduitId = article.ProduitId,
        ServiceId = article.ServiceId,
        Designation = article.Designation,
        QuantiteCommandee = 1,
        Quantite = 1,
        PrixUnitaireHt = article.PrixUnitaireHT,
        Remise = 0,
        TauxTva = article.TauxTVA,
    };
}
