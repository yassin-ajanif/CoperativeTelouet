using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Vente.Facturation;

public partial class FacturationViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Facturation";
}
