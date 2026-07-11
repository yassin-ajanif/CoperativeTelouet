using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CoperativeTelouet.UI.ViewModels.Dialogs;

public partial class MessageDialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private bool _isError;

    public Action? CloseAction { get; set; }

    [RelayCommand]
    private void Ok() => CloseAction?.Invoke();
}
