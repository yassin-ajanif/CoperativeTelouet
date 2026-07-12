using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class ChambreOccupancyItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private int _capacite;

    [ObservableProperty]
    private int _occupes;

    public double OccupancyRatio => Capacite <= 0 ? 0 : (double)Occupes / Capacite;

    public string OccupancyLabel => $"{Occupes} / {Capacite}";

    partial void OnCapaciteChanged(int value) => NotifyOccupancy();

    partial void OnOccupesChanged(int value) => NotifyOccupancy();

    private void NotifyOccupancy()
    {
        OnPropertyChanged(nameof(OccupancyRatio));
        OnPropertyChanged(nameof(OccupancyLabel));
    }
}
