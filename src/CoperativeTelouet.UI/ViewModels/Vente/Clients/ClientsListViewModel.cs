using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services.Client;

namespace CoperativeTelouet.UI.ViewModels.Vente.Clients;

public partial class ClientsListViewModel : ViewModelBase
{
    private readonly IClientService _clients;
    private ClientsViewModel? _host;
    private IReadOnlyList<TiersDto> _all = [];

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

    public ClientsListViewModel(IClientService clients)
    {
        _clients = clients;
    }

    public void AttachHost(ClientsViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));

    partial void OnSearchTextChanged(string value) => _ = SearchAsync();

    [RelayCommand]
    public async Task LoadAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;
            _all = await _clients.GetClientsAsync(string.IsNullOrWhiteSpace(SearchText) ? null : SearchText);
            TotalCount = _all.Count;
            TotalPages = Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
            if (PageIndex > TotalPages)
                PageIndex = TotalPages;
            ApplyPage();
            OnPropertyChanged(nameof(CountLabel));
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
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
            await _clients.ToggleActifAsync(SelectedItem.Id);
            await LoadAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void PreviousPage()
    {
        if (!CanGoPrevious) return;
        PageIndex--;
        ApplyPage();
    }

    [RelayCommand]
    private void NextPage()
    {
        if (!CanGoNext) return;
        PageIndex++;
        ApplyPage();
    }

    private void ApplyPage()
    {
        var slice = _all.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        PageItems = new ObservableCollection<TiersDto>(slice);
        OnPropertyChanged(nameof(PageLabel));
        OnPropertyChanged(nameof(CanGoPrevious));
        OnPropertyChanged(nameof(CanGoNext));
    }
}
