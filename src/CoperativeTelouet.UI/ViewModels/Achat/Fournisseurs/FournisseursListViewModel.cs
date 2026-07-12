using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services.Fournisseur;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Achat.Fournisseurs;

public partial class FournisseursListViewModel : ViewModelBase
{
    private readonly IFournisseurService _fournisseurs;
    private readonly IUserDialogService _dialogs;
    private FournisseursViewModel? _host;

    [ObservableProperty]
    private ObservableCollection<TiersDto> _pageItems = [];

    [ObservableProperty]
    private TiersDto? _selectedItem;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PageLabel))]
    [NotifyPropertyChangedFor(nameof(CanGoPrevious))]
    [NotifyPropertyChangedFor(nameof(CanGoNext))]
    private int _pageIndex = 1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PageLabel))]
    [NotifyPropertyChangedFor(nameof(CanGoPrevious))]
    [NotifyPropertyChangedFor(nameof(CanGoNext))]
    private int _totalPages = 1;

    [ObservableProperty]
    private int _totalCount;

    private const int PageSize = 15;

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    public string PageLabel => $"{PageIndex} / {TotalPages}";
    public bool CanGoPrevious => PageIndex > 1;
    public bool CanGoNext => PageIndex < TotalPages;
    public string CountLabel => TotalCount <= 1 ? $"{TotalCount} élément" : $"{TotalCount} éléments";

    public FournisseursListViewModel(IFournisseurService fournisseurs, IUserDialogService dialogs)
    {
        _fournisseurs = fournisseurs;
        _dialogs = dialogs;
    }

    public void AttachHost(FournisseursViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));

    partial void OnSearchTextChanged(string value) => _ = SearchAsync();

    [RelayCommand]
    public async Task LoadAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            var result = await _fournisseurs.GetFournisseursAsync(
                string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                PageIndex,
                PageSize);

            TotalCount = result.TotalCount;
            TotalPages = Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
            if (PageIndex > TotalPages)
            {
                PageIndex = TotalPages;
                result = await _fournisseurs.GetFournisseursAsync(
                    string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                    PageIndex,
                    PageSize);
                TotalCount = result.TotalCount;
            }

            PageItems = new ObservableCollection<TiersDto>(result.Items);
            OnPropertyChanged(nameof(CountLabel));
            OnPropertyChanged(nameof(PageLabel));
            OnPropertyChanged(nameof(CanGoPrevious));
            OnPropertyChanged(nameof(CanGoNext));
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
    private async Task SearchAsync()
    {
        PageIndex = 1;
        await LoadAsync();
    }

    [RelayCommand]
    private void Nouveau() => _host?.ShowDetail(null);

    [RelayCommand]
    private void OpenSelected()
    {
        if (SelectedItem is not null)
            _host?.ShowDetail(SelectedItem.Id);
    }

    [RelayCommand]
    private async Task ToggleActifAsync()
    {
        if (SelectedItem is null)
            return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;
            await _fournisseurs.ToggleActifAsync(SelectedItem.Id);
            await LoadAsync();
            await _dialogs.ShowSuccessAsync("Statut du fournisseur mis à jour.");
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
            await _fournisseurs.DeleteFournisseurAsync(SelectedItem.Id);
            await LoadAsync();
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

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (!CanGoPrevious) return;
        PageIndex--;
        await LoadAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if (!CanGoNext) return;
        PageIndex++;
        await LoadAsync();
    }
}
