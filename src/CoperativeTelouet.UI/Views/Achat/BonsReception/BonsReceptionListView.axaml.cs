using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Achat.BonsReception;

namespace CoperativeTelouet.UI.Views.Achat.BonsReception;

public partial class BonsReceptionListView : UserControl
{
    public BonsReceptionListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is BonsReceptionListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
