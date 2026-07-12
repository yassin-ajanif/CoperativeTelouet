using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class BonSortieListItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _clientNom = string.Empty;

    [ObservableProperty]
    private DateTime _dateSortie;

    [ObservableProperty]
    private string _etatBac = string.Empty;

    [ObservableProperty]
    private int _nombreBacs;

    [ObservableProperty]
    private string _bonEntreeNumero = "-";

    [ObservableProperty]
    private string _factureNumero = "-";

    public string DateDisplay => DateSortie.ToString("dd/MM/yyyy");
}
