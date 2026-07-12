using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CoperativeTelouet.UI.ViewModels.Vente.Devis;

namespace CoperativeTelouet.UI.Views.Vente.Devis;

public partial class DevisListView : UserControl
{
    public DevisListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is DevisListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
