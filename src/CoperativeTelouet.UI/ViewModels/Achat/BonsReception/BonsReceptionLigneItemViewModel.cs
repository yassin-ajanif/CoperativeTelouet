using CommunityToolkit.Mvvm.ComponentModel;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur.BonCommande;
using CoperativeTelouet.Business.Services.Fournisseur.BonReception;

namespace CoperativeTelouet.UI.ViewModels.Achat.BonsReception;

public partial class BonsReceptionLigneItemViewModel : ObservableObject
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
    private decimal _quantiteRecue = 1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MontantHt))]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private decimal _prixUnitaireHt;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MontantTtc))]
    private decimal _tauxTva;

    public decimal MontantHt => IBonReceptionFournisseurService.LineHt(QuantiteRecue, PrixUnitaireHt);
    public decimal MontantTtc => IBonReceptionFournisseurService.LineTtc(QuantiteRecue, PrixUnitaireHt, TauxTva);

    public CreateBonReceptionFournisseurLigneDto ToCreateDto() => new(
        ProduitId,
        ServiceId,
        Designation.Trim(),
        QuantiteRecue,
        PrixUnitaireHt,
        TauxTva);

    public static BonsReceptionLigneItemViewModel FromDto(BonReceptionFournisseurLigneDto dto) => new()
    {
        ProduitId = dto.ProduitId,
        ServiceId = dto.ServiceId,
        Designation = dto.Designation,
        QuantiteRecue = dto.QuantiteRecue,
        PrixUnitaireHt = dto.PrixUnitaireHT,
        TauxTva = dto.TauxTVA,
    };

    public static BonsReceptionLigneItemViewModel FromArticle(ArticleSuggestionDto article) => new()
    {
        ProduitId = article.ProduitId,
        ServiceId = article.ServiceId,
        Designation = article.Designation,
        QuantiteRecue = 1,
        PrixUnitaireHt = article.PrixUnitaireHT,
        TauxTva = article.TauxTVA,
    };
}
