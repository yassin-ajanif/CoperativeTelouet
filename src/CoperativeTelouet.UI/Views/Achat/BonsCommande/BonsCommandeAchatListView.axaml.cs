using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Achat.BonsCommande;

namespace CoperativeTelouet.UI.Views.Achat.BonsCommande;

public partial class BonsCommandeAchatListView : UserControl
{
    public BonsCommandeAchatListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is BonsCommandeAchatListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
