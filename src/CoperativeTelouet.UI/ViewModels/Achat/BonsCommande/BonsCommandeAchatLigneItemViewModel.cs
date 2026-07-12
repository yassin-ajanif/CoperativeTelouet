using CommunityToolkit.Mvvm.ComponentModel;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur.BonCommande;

namespace CoperativeTelouet.UI.ViewModels.Achat.BonsCommande;

public partial class BonsCommandeAchatLigneItemViewModel : ObservableObject
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
    [NotifyPropertyChangedFor(nameof(MontantHt))]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private decimal _quantiteCommandee = 1;

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

    [ObservableProperty]
    private string? _conditionnement;

    public decimal MontantHt => IBonCommandeFournisseurService.LineHt(QuantiteCommandee, PrixUnitaireHt, Remise);
    public decimal MontantTtc => IBonCommandeFournisseurService.LineTtc(QuantiteCommandee, PrixUnitaireHt, Remise, TauxTva);

    public CreateBonCommandeFournisseurLigneDto ToCreateDto() => new(
        ProduitId,
        ServiceId,
        Designation.Trim(),
        QuantiteCommandee,
        PrixUnitaireHt,
        Remise,
        TauxTva,
        string.IsNullOrWhiteSpace(Conditionnement) ? null : Conditionnement.Trim());

    public static BonsCommandeAchatLigneItemViewModel FromDto(BonCommandeFournisseurLigneDto dto) => new()
    {
        ProduitId = dto.ProduitId,
        ServiceId = dto.ServiceId,
        Designation = dto.Designation,
        QuantiteCommandee = dto.QuantiteCommandee,
        PrixUnitaireHt = dto.PrixUnitaireHT,
        Remise = dto.Remise,
        TauxTva = dto.TauxTVA,
        Conditionnement = dto.Conditionnement,
    };

    public static BonsCommandeAchatLigneItemViewModel FromArticle(ArticleSuggestionDto article) => new()
    {
        ProduitId = article.ProduitId,
        ServiceId = article.ServiceId,
        Designation = article.Designation,
        QuantiteCommandee = 1,
        PrixUnitaireHt = article.PrixUnitaireHT,
        Remise = 0,
        TauxTva = article.TauxTVA,
        Conditionnement = article.Unite,
    };
}
