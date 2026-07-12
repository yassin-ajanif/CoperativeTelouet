using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class LotEnCoursItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _numeroLot = string.Empty;

    [ObservableProperty]
    private string _clientNom = string.Empty;

    [ObservableProperty]
    private int _nombreBacs;

    [ObservableProperty]
    private string _chambreNom = string.Empty;
}
