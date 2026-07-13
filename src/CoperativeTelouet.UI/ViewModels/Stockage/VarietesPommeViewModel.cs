using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services.Stockage;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class VarietesPommeViewModel : ViewModelBase
{
    private readonly IVarietePommeService _service;
    private readonly IUserDialogService _dialogs;

    [ObservableProperty]
    private ObservableCollection<VarietePommeDto> _items = [];

    [ObservableProperty]
    private VarietePommeDto? _selectedItem;

    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private bool _isBusy;

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public VarietesPommeViewModel(IVarietePommeService service, IUserDialogService dialogs)
    {
        _service = service;
        _dialogs = dialogs;
        _ = LoadAsync();
    }

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));

    partial void OnSelectedItemChanged(VarietePommeDto? value)
    {
        Nom = value?.Nom ?? string.Empty;
        ErrorMessage = null;
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;
            var list = await _service.GetVarietesAsync();
            Items = new ObservableCollection<VarietePommeDto>(list);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            await _dialogs.ShowErrorAsync(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void Nouveau()
    {
        SelectedItem = null;
        Nom = string.Empty;
        ErrorMessage = null;
    }

    [RelayCommand]
    private async Task EnregistrerAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            if (SelectedItem is null)
            {
                await _service.CreateVarieteAsync(new CreateVarietePommeDto(Nom.Trim()));
            }
            else
            {
                await _service.UpdateVarieteAsync(SelectedItem.Id, new UpdateVarietePommeDto(Nom.Trim()));
            }

            await LoadAsync();
            Nouveau();
            await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
        }
        catch (ValidationException ex)
        {
            var msg = string.Join(Environment.NewLine, ex.Errors.Select(e => e.ErrorMessage));
            ErrorMessage = msg;
            await _dialogs.ShowErrorAsync(msg);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            await _dialogs.ShowErrorAsync(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SupprimerAsync()
    {
        if (SelectedItem is null)
            return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;
            await _service.DeleteVarieteAsync(SelectedItem.Id);
            await LoadAsync();
            Nouveau();
            await _dialogs.ShowSuccessAsync("Suppression réussie.");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            await _dialogs.ShowErrorAsync(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
