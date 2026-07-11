using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CoperativeTelouet.UI.ViewModels.Dialogs;
using CoperativeTelouet.UI.Views.Dialogs;

namespace CoperativeTelouet.UI.Services;

/// <summary>
/// UI-layer only: opens <see cref="MessageDialogView"/>. Never call from Business.
/// </summary>
public sealed class UserDialogService : IUserDialogService
{
    public Task ShowSuccessAsync(string message) =>
        ShowAsync("Succès", message, isError: false);

    public Task ShowErrorAsync(string message) =>
        ShowAsync("Erreur", message, isError: true);

    private static async Task ShowAsync(string title, string message, bool isError)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow is null)
            return;

        var vm = new MessageDialogViewModel
        {
            Title = title,
            Message = message,
            IsError = isError,
        };

        var dialog = new MessageDialogView
        {
            DataContext = vm,
        };
        vm.CloseAction = () => dialog.Close();

        await dialog.ShowDialog(desktop.MainWindow);
    }
}
