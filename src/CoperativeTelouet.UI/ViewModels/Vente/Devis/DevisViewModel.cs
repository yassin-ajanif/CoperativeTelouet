using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Vente.Devis;

public partial class DevisViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Devis";
}
