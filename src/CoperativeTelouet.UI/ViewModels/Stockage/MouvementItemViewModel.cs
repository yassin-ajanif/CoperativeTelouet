using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class MouvementItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private string _type = string.Empty;

    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _clientNom = string.Empty;

    [ObservableProperty]
    private string _etatBac = string.Empty;

    [ObservableProperty]
    private int _nombreBacs;

    [ObservableProperty]
    private string _lotOuTiret = "-";

    public string DateDisplay => Date.ToString("dd/MM/yyyy");
}
