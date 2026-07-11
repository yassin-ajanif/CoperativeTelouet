using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Vente.BonsLivraison;

public partial class BonsLivraisonViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Bons de livraison";
}
