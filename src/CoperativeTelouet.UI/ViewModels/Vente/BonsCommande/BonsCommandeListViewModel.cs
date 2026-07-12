using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.Services.Client.BonCommande;
using CoperativeTelouet.UI.Services;

namespace CoperativeTelouet.UI.ViewModels.Vente.BonsCommande;

public partial class BonsCommandeListViewModel : ViewModelBase
{
    private readonly IBonCommandeClientService _bons;
    private readonly IUserDialogService _dialogs;
    private BonsCommandeViewModel? _host;

    [ObservableProperty]
    private ObservableCollection<BonCommandeClientListItemDto> _pageItems = [];

    [ObservableProperty]
    private BonCommandeClientListItemDto? _selectedItem;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _dateFromText = string.Empty;

    [ObservableProperty]
    private string _dateToText = string.Empty;

    [ObservableProperty]
    private bool _showDateFilter;

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

    public BonsCommandeListViewModel(IBonCommandeClientService bons, IUserDialogService dialogs)
    {
        _bons = bons;
        _dialogs = dialogs;
    }

    public void AttachHost(BonsCommandeViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));

    partial void OnSearchTextChanged(string value) => _ = SearchAsync();

    [RelayCommand]
    public async Task LoadAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            var result = await _bons.GetBonsCommandeAsync(
                string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                TryParseDate(DateFromText),
                TryParseDate(DateToText),
                PageIndex,
                PageSize);

            TotalCount = result.TotalCount;
            TotalPages = Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
            if (PageIndex > TotalPages)
            {
                PageIndex = TotalPages;
                result = await _bons.GetBonsCommandeAsync(
                    string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                    TryParseDate(DateFromText),
                    TryParseDate(DateToText),
                    PageIndex,
                    PageSize);
                TotalCount = result.TotalCount;
            }

            PageItems = new ObservableCollection<BonCommandeClientListItemDto>(result.Items);
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
    private void ToggleDateFilter() => ShowDateFilter = !ShowDateFilter;

    [RelayCommand]
    private async Task ApplyDateFilterAsync()
    {
        PageIndex = 1;
        await LoadAsync();
    }

    [RelayCommand]
    private async Task ClearDateFilterAsync()
    {
        DateFromText = string.Empty;
        DateToText = string.Empty;
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

    private static DateTime? TryParseDate(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        return DateTime.TryParse(text, out var date) ? date.Date : null;
    }
}