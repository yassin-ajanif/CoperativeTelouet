using CommunityToolkit.Mvvm.ComponentModel;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur.Facture;

namespace CoperativeTelouet.UI.ViewModels.Achat.Facturation;

public partial class FacturationAchatLigneItemViewModel : ObservableObject
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

    [ObservableProperty]
    private string? _conditionnement;

    public decimal MontantHt => IFactureFournisseurService.LineHt(Quantite, PrixUnitaireHt, Remise);
    public decimal MontantTtc => IFactureFournisseurService.LineTtc(Quantite, PrixUnitaireHt, Remise, TauxTva);

    public CreateFactureFournisseurLigneDto ToCreateDto() => new(
        null,
        ProduitId,
        ServiceId,
        Designation.Trim(),
        Quantite,
        PrixUnitaireHt,
        Remise,
        TauxTva,
        string.IsNullOrWhiteSpace(Conditionnement) ? null : Conditionnement.Trim());

    public static FacturationAchatLigneItemViewModel FromDto(FactureFournisseurLigneDto dto) => new()
    {
        ProduitId = dto.ProduitId,
        ServiceId = dto.ServiceId,
        Designation = dto.Designation,
        Quantite = dto.Quantite,
        PrixUnitaireHt = dto.PrixUnitaireHT,
        Remise = dto.Remise,
        TauxTva = dto.TauxTVA,
        Conditionnement = dto.Conditionnement,
    };

    public static FacturationAchatLigneItemViewModel FromArticle(ArticleSuggestionDto article) => new()
    {
        ProduitId = article.ProduitId,
        ServiceId = article.ServiceId,
        Designation = article.Designation,
        Quantite = 1,
        PrixUnitaireHt = article.PrixUnitaireHT,
        Remise = 0,
        TauxTva = article.TauxTVA,
        Conditionnement = article.Unite,
    };
}
