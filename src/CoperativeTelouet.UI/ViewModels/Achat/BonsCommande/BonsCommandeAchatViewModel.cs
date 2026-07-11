using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Achat.BonsCommande;

public partial class BonsCommandeAchatViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Bons de commande";
}
