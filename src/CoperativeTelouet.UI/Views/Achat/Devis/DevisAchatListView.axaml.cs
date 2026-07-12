using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Achat.Devis;

namespace CoperativeTelouet.UI.Views.Achat.Devis;

public partial class DevisAchatListView : UserControl
{
    public DevisAchatListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is DevisAchatListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
