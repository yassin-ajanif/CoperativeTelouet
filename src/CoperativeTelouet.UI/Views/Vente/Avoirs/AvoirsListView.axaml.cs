using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Vente.Avoirs;

namespace CoperativeTelouet.UI.Views.Vente.Avoirs;

public partial class AvoirsListView : UserControl
{
    public AvoirsListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is AvoirsListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
