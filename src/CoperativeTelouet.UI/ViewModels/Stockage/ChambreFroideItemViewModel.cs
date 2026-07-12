using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class ChambreFroideItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private int _capaciteBacs;

    [ObservableProperty]
    private bool _actif = true;

    public string ActifLabel => Actif ? "Oui" : "Non";

    partial void OnActifChanged(bool value) => OnPropertyChanged(nameof(ActifLabel));
}
