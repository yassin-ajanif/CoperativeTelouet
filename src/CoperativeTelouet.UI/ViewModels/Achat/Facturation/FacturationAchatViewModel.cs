using CommunityToolkit.Mvvm.ComponentModel;

namespace CoperativeTelouet.UI.ViewModels.Achat.Facturation;

public partial class FacturationAchatViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Facturation achat";
}
