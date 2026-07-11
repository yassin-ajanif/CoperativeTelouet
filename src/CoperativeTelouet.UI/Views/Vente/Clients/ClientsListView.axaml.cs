using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CoperativeTelouet.UI.ViewModels.Vente.Clients;

namespace CoperativeTelouet.UI.Views.Vente.Clients;

public partial class ClientsListView : UserControl
{
    public ClientsListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is ClientsListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
