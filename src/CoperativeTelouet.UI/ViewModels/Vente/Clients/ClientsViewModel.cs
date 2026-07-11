using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Vente.Clients;

public partial class ClientsViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Clients";
}
