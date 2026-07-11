using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Achat.Fournisseurs;

public partial class FournisseursViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Fournisseurs";
}
