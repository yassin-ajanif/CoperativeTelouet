using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Vente.BonsLivraison;

namespace CoperativeTelouet.UI.Views.Vente.BonsLivraison;

public partial class BonsLivraisonListView : UserControl
{
    public BonsLivraisonListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is BonsLivraisonListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
