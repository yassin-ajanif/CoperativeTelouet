using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services.Catalog;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels;

public partial class ProduitsViewModel : ViewModelBase
{
    private readonly IProduitService _produits;
    private readonly IUserDialogService _dialogs;
    private int? _editingId;

    [ObservableProperty]
    private ObservableCollection<ProduitDto> _pageItems = [];

    [ObservableProperty]
    private ProduitDto? _selectedItem;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _reference = string.Empty;

    [ObservableProperty]
    private string _designation = string.Empty;

    [ObservableProperty]
    private string? _codeBarre;

    [ObservableProperty]
    private string? _unite = "U";

    [ObservableProperty]
    private decimal _stockActuel;

    [ObservableProperty]
    private decimal _prixAchatHt;

    [ObservableProperty]
    private decimal _prixVenteHt;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrixAchatTtc))]
    [NotifyPropertyChangedFor(nameof(PrixVenteTtc))]
    private decimal _tauxTva = 20m;

    [ObservableProperty]
    private decimal _stockMinimum;

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
    public string FicheTitle => IsNew ? "Fiche produit (brouillon)" : "Fiche produit";

    public decimal PrixAchatTtc
    {
        get => RoundMoney(PrixAchatHt * (1 + TauxTva / 100m));
        set
        {
            var ht = TauxTva <= -100 ? 0 : value / (1 + TauxTva / 100m);
            if (PrixAchatHt != ht)
                PrixAchatHt = RoundMoney(ht);
        }
    }

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

    public ProduitsViewModel(IProduitService produits, IUserDialogService dialogs)
    {
        _produits = produits;
        _dialogs = dialogs;
        _ = LoadAsync();
    }

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnSearchTextChanged(string value) => _ = SearchAsync();
    partial void OnPrixAchatHtChanged(decimal value) => OnPropertyChanged(nameof(PrixAchatTtc));
    partial void OnPrixVenteHtChanged(decimal value) => OnPropertyChanged(nameof(PrixVenteTtc));

    partial void OnSelectedItemChanged(ProduitDto? value)
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

            var result = await _produits.GetProduitsAsync(
                string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                PageIndex,
                PageSize);

            TotalCount = result.TotalCount;
            TotalPages = Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
            if (PageIndex > TotalPages)
            {
                PageIndex = TotalPages;
                result = await _produits.GetProduitsAsync(
                    string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                    PageIndex,
                    PageSize);
                TotalCount = result.TotalCount;
            }

            PageItems = new ObservableCollection<ProduitDto>(result.Items);
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
        CodeBarre = null;
        Unite = "U";
        StockActuel = 0;
        PrixAchatHt = 0;
        PrixVenteHt = 0;
        TauxTva = 20m;
        StockMinimum = 0;
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
                var created = await _produits.CreateProduitAsync(new CreateProduitDto(
                    Reference.Trim(),
                    NullIfEmpty(CodeBarre),
                    Designation.Trim(),
                    NullIfEmpty(Unite),
                    PrixAchatHt,
                    PrixVenteHt,
                    TauxTva,
                    0,
                    StockMinimum,
                    null,
                    Actif));
                await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
                await LoadAsync();
                SelectedItem = PageItems.FirstOrDefault(p => p.Id == created.Id) ?? created;
            }
            else
            {
                await _produits.UpdateProduitAsync(_editingId.Value, new UpdateProduitDto(
                    Reference.Trim(),
                    NullIfEmpty(CodeBarre),
                    Designation.Trim(),
                    NullIfEmpty(Unite),
                    PrixAchatHt,
                    PrixVenteHt,
                    TauxTva,
                    StockActuel,
                    StockMinimum,
                    SelectedItem?.CategorieId,
                    Actif,
                    SelectedItem?.ImageData));
                await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
                var id = _editingId.Value;
                await LoadAsync();
                SelectedItem = PageItems.FirstOrDefault(p => p.Id == id);
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
            await _produits.DeleteProduitAsync(_editingId.Value);
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
    private async Task ExportCsvAsync()
        => await _dialogs.ShowErrorAsync("Export CSV bientôt disponible.");

    [RelayCommand]
    private async Task ImportCsvAsync()
        => await _dialogs.ShowErrorAsync("Import CSV bientôt disponible.");

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

    private void LoadFiche(ProduitDto dto)
    {
        _editingId = dto.Id;
        Reference = dto.Reference;
        Designation = dto.Designation;
        CodeBarre = dto.CodeBarre;
        Unite = dto.Unite;
        StockActuel = dto.StockActuel;
        PrixAchatHt = dto.PrixAchatHT;
        PrixVenteHt = dto.PrixVenteHT;
        TauxTva = dto.TauxTVA;
        StockMinimum = dto.StockMinimum;
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
