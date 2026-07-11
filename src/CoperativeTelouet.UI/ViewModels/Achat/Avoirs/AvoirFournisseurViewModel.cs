using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Achat.Avoirs;

public partial class AvoirFournisseurViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Avoir fournisseur";
}
