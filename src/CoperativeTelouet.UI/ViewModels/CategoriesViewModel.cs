using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels;

public partial class CategoriesViewModel : ViewModelBase
{
    private readonly IGenericService<Categorie, CategorieDto, CreateCategorieDto, UpdateCategorieDto> _service;
    private readonly IUserDialogService _dialogs;

    [ObservableProperty]
    private ObservableCollection<CategorieDto> _items = [];

    [ObservableProperty]
    private CategorieDto? _selectedItem;

    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private bool _isBusy;

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public CategoriesViewModel(
        IGenericService<Categorie, CategorieDto, CreateCategorieDto, UpdateCategorieDto> service,
        IUserDialogService dialogs)
    {
        _service = service;
        _dialogs = dialogs;
        _ = LoadAsync();
    }

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));

    partial void OnSelectedItemChanged(CategorieDto? value)
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
            var list = await _service.GetAllAsync();
            Items = new ObservableCollection<CategorieDto>(list.OrderBy(x => x.Nom));
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
    private void New()
    {
        SelectedItem = null;
        Nom = string.Empty;
        ErrorMessage = null;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            if (SelectedItem is null)
            {
                await _service.CreateAsync(new CreateCategorieDto(Nom.Trim()));
            }
            else
            {
                await _service.UpdateAsync(SelectedItem.Id, new UpdateCategorieDto(Nom.Trim()));
            }

            await LoadAsync();
            New();
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
    private async Task DeleteAsync()
    {
        if (SelectedItem is null)
            return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;
            await _service.DeleteAsync(SelectedItem.Id);
            await LoadAsync();
            New();
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
