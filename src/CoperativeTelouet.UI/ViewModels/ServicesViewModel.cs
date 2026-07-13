using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services.Catalog;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels;

public partial class ServicesViewModel : ViewModelBase
{
    private readonly IServiceItemService _services;
    private readonly IUserDialogService _dialogs;
    private int? _editingId;

    [ObservableProperty]
    private ObservableCollection<ServiceItemDto> _pageItems = [];

    [ObservableProperty]
    private ServiceItemDto? _selectedItem;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _reference = string.Empty;

    [ObservableProperty]
    private string _designation = string.Empty;

    [ObservableProperty]
    private string? _unite = "U";

    [ObservableProperty]
    private decimal _prixVenteHt;

    [ObservableProperty]
    private decimal _coutHt;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrixVenteTtc))]
    private decimal _tauxTva = 20m;

    [ObservableProperty]
    private string? _note;

    [ObservableProperty]
    private bool _actif = true;

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
    public bool IsNew => _editingId is null;
    public string PageLabel => $"{PageIndex} / {TotalPages}";
    public bool CanGoPrevious => PageIndex > 1;
    public bool CanGoNext => PageIndex < TotalPages;
    public string CountLabel => TotalCount <= 1 ? $"{TotalCount} élément" : $"{TotalCount} éléments";
    public string FicheTitle => IsNew ? "Fiche service (brouillon)" : "Fiche service";

    public decimal PrixVenteTtc
    {
        get => RoundMoney(PrixVenteHt * (1 + TauxTva / 100m));
        set
        {
            var ht = TauxTva <= -100 ? 0 : value / (1 + TauxTva / 100m);
            if (PrixVenteHt != ht)
                PrixVenteHt = RoundMoney(ht);
        }
    }

    public ServicesViewModel(IServiceItemService services, IUserDialogService dialogs)
    {
        _services = services;
        _dialogs = dialogs;
        _ = LoadAsync();
    }

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnSearchTextChanged(string value) => _ = SearchAsync();
    partial void OnPrixVenteHtChanged(decimal value) => OnPropertyChanged(nameof(PrixVenteTtc));

    partial void OnSelectedItemChanged(ServiceItemDto? value)
    {
        if (value is null)
            return;
        LoadFiche(value);
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            var result = await _services.GetServicesAsync(
                string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                PageIndex,
                PageSize);

            TotalCount = result.TotalCount;
            TotalPages = Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
            if (PageIndex > TotalPages)
            {
                PageIndex = TotalPages;
                result = await _services.GetServicesAsync(
                    string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                    PageIndex,
                    PageSize);
                TotalCount = result.TotalCount;
            }

            PageItems = new ObservableCollection<ServiceItemDto>(result.Items);
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
    private void Nouveau()
    {
        SelectedItem = null;
        _editingId = null;
        Reference = string.Empty;
        Designation = string.Empty;
        Unite = "U";
        PrixVenteHt = 0;
        CoutHt = 0;
        TauxTva = 20m;
        Note = null;
        Actif = true;
        ErrorMessage = null;
        OnPropertyChanged(nameof(IsNew));
        OnPropertyChanged(nameof(FicheTitle));
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            if (_editingId is null)
            {
                var created = await _services.CreateServiceAsync(new CreateServiceItemDto(
                    Reference.Trim(),
                    Designation.Trim(),
                    NullIfEmpty(Unite),
                    PrixVenteHt,
                    CoutHt,
                    TauxTva,
                    Actif,
                    NullIfEmpty(Note)));
                await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
                await LoadAsync();
                SelectedItem = PageItems.FirstOrDefault(s => s.Id == created.Id) ?? created;
            }
            else
            {
                await _services.UpdateServiceAsync(_editingId.Value, new UpdateServiceItemDto(
                    Reference.Trim(),
                    Designation.Trim(),
                    NullIfEmpty(Unite),
                    PrixVenteHt,
                    CoutHt,
                    TauxTva,
                    Actif,
                    NullIfEmpty(Note),
                    SelectedItem?.ImageData));
                await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
                var id = _editingId.Value;
                await LoadAsync();
                SelectedItem = PageItems.FirstOrDefault(s => s.Id == id);
            }
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
        if (_editingId is null)
            return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;
            await _services.DeleteServiceAsync(_editingId.Value);
            await _dialogs.ShowSuccessAsync("Suppression réussie.");
            Nouveau();
            await LoadAsync();
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

    private void LoadFiche(ServiceItemDto dto)
    {
        _editingId = dto.Id;
        Reference = dto.Reference;
        Designation = dto.Designation;
        Unite = dto.Unite;
        PrixVenteHt = dto.PrixVenteHT;
        CoutHt = dto.CoutHT;
        TauxTva = dto.TauxTVA;
        Note = dto.Note;
        Actif = dto.Actif;
        ErrorMessage = null;
        OnPropertyChanged(nameof(IsNew));
        OnPropertyChanged(nameof(FicheTitle));
    }

    private static string? NullIfEmpty(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static decimal RoundMoney(decimal value) =>
        Math.Round(value, 2, MidpointRounding.AwayFromZero);
}
