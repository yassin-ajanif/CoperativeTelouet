using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class BonEntreeListItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _clientNom = string.Empty;

    [ObservableProperty]
    private DateTime _dateEntree;

    [ObservableProperty]
    private string _etatBac = string.Empty;

    [ObservableProperty]
    private int _nombreBacs;

    [ObservableProperty]
    private string _chambreNom = "-";

    [ObservableProperty]
    private string _varieteNom = "-";

    [ObservableProperty]
    private string _numeroLot = "-";

    public string DateDisplay => DateEntree.ToString("dd/MM/yyyy");
}
