using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services.Stockage;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class ChambresFroidesViewModel : ViewModelBase
{
    private readonly IChambreFroideService _service;
    private readonly IUserDialogService _dialogs;

    [ObservableProperty]
    private ObservableCollection<ChambreFroideDto> _items = [];

    [ObservableProperty]
    private ChambreFroideDto? _selectedItem;

    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private string _capaciteText = "200";

    [ObservableProperty]
    private bool _actif = true;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private bool _isBusy;

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public ChambresFroidesViewModel(IChambreFroideService service, IUserDialogService dialogs)
    {
        _service = service;
        _dialogs = dialogs;
        _ = LoadAsync();
    }

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));

    partial void OnSelectedItemChanged(ChambreFroideDto? value)
    {
        Nom = value?.Nom ?? string.Empty;
        CapaciteText = value?.CapaciteBacs.ToString() ?? "200";
        Actif = value?.Actif ?? true;
        ErrorMessage = null;
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;
            var list = await _service.GetChambresAsync();
            Items = new ObservableCollection<ChambreFroideDto>(list);
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
        CapaciteText = "200";
        Actif = true;
        ErrorMessage = null;
    }

    [RelayCommand]
    private async Task EnregistrerAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            if (!int.TryParse(CapaciteText, out var capacite))
            {
                ErrorMessage = "Capacité invalide.";
                await _dialogs.ShowErrorAsync(ErrorMessage);
                return;
            }

            if (SelectedItem is null)
            {
                await _service.CreateChambreAsync(new CreateChambreFroideDto(Nom.Trim(), capacite, Actif));
            }
            else
            {
                await _service.UpdateChambreAsync(
                    SelectedItem.Id,
                    new UpdateChambreFroideDto(Nom.Trim(), capacite, Actif));
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
            await _service.DeleteChambreAsync(SelectedItem.Id);
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
