using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Achat.Avoirs;

namespace CoperativeTelouet.UI.Views.Achat.Avoirs;

public partial class AvoirFournisseurListView : UserControl
{
    public AvoirFournisseurListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is AvoirFournisseurListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
