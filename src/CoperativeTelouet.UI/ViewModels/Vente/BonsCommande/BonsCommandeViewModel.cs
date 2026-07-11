using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Vente.BonsCommande;

public partial class BonsCommandeViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Bons de commande";
}
