using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Vente.BonsCommande;

namespace CoperativeTelouet.UI.Views.Vente.BonsCommande;

public partial class BonsCommandeListView : UserControl
{
    public BonsCommandeListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is BonsCommandeListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
