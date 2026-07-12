using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class StockageDashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _bacsVides = 420;

    [ObservableProperty]
    private int _bacsPleins = 180;

    [ObservableProperty]
    private int _totalBacs = 600;

    [ObservableProperty]
    private string _occupationPercentLabel = "72 %";

    [ObservableProperty]
    private string _chambresResume = "3 chambres";

    [ObservableProperty]
    private string _tarifParJourLabel = "2,50 DH";

    [ObservableProperty]
    private string _actionHint = "Données mock — Business à brancher plus tard.";

    public ObservableCollection<ChambreOccupancyItemViewModel> Chambres { get; } = [];
    public ObservableCollection<LotEnCoursItemViewModel> LotsEnCours { get; } = [];
    public ObservableCollection<MouvementItemViewModel> Mouvements { get; } = [];

    public string BacsVidesSubtitle => $"/ {TotalBacs} total";

    public StockageDashboardViewModel()
    {
        LoadMockData();
    }

    private void LoadMockData()
    {
        Chambres.Clear();
        Chambres.Add(new ChambreOccupancyItemViewModel { Nom = "CF-1 Nord", Capacite = 200, Occupes = 160 });
        Chambres.Add(new ChambreOccupancyItemViewModel { Nom = "CF-2 Sud", Capacite = 200, Occupes = 120 });
        Chambres.Add(new ChambreOccupancyItemViewModel { Nom = "CF-3 Est", Capacite = 200, Occupes = 80 });

        LotsEnCours.Clear();
        LotsEnCours.Add(new LotEnCoursItemViewModel
        {
            NumeroLot = "L-042",
            ClientNom = "Alami",
            NombreBacs = 40,
            ChambreNom = "CF-1 Nord"
        });
        LotsEnCours.Add(new LotEnCoursItemViewModel
        {
            NumeroLot = "L-043",
            ClientNom = "Benali",
            NombreBacs = 25,
            ChambreNom = "CF-2 Sud"
        });
        LotsEnCours.Add(new LotEnCoursItemViewModel
        {
            NumeroLot = "L-044",
            ClientNom = "Idri",
            NombreBacs = 55,
            ChambreNom = "CF-1 Nord"
        });

        Mouvements.Clear();
        Mouvements.Add(new MouvementItemViewModel
        {
            Date = new DateTime(2026, 7, 12),
            Type = "Entrée",
            Numero = "BE-2026-12",
            ClientNom = "Alami",
            EtatBac = "Plein",
            NombreBacs = 40,
            LotOuTiret = "L-042"
        });
        Mouvements.Add(new MouvementItemViewModel
        {
            Date = new DateTime(2026, 7, 12),
            Type = "Sortie",
            Numero = "BS-2026-08",
            ClientNom = "Benali",
            EtatBac = "Plein",
            NombreBacs = 20,
            LotOuTiret = "L-039"
        });
        Mouvements.Add(new MouvementItemViewModel
        {
            Date = new DateTime(2026, 7, 11),
            Type = "Entrée",
            Numero = "BE-2026-11",
            ClientNom = "Idri",
            EtatBac = "Vide",
            NombreBacs = 15,
            LotOuTiret = "-"
        });
    }

    [RelayCommand]
    private void NouveauBonEntree() =>
        ActionHint = "Action : Bon d'entrée — écran à venir (mock).";

    [RelayCommand]
    private void NouveauBonSortie() =>
        ActionHint = "Action : Bon de sortie — écran à venir (mock).";

    [RelayCommand]
    private void GererChambres() =>
        ActionHint = "Action : Gérer chambres — écran à venir (mock).";
}
