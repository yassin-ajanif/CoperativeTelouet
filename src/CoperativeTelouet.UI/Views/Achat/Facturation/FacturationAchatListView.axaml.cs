using Avalonia.Controls;
using Avalonia.Input;
using CoperativeTelouet.UI.ViewModels.Achat.Facturation;

namespace CoperativeTelouet.UI.Views.Achat.Facturation;

public partial class FacturationAchatListView : UserControl
{
    public FacturationAchatListView()
    {
        InitializeComponent();
    }

    private void OnRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is FacturationAchatListViewModel vm && vm.OpenSelectedCommand.CanExecute(null))
            vm.OpenSelectedCommand.Execute(null);
    }
}
