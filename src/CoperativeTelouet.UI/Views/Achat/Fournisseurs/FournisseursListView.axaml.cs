using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Achat.Fournisseurs;

namespace CoperativeTelouet.UI.Views.Achat.Fournisseurs;

public partial class FournisseursListView : UserControl
{
    public FournisseursListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is FournisseursListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
