using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels;

public partial class AccueilViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Accueil";
}
