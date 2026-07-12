using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur.BonCommande;
using CoperativeTelouet.Business.Services.Fournisseur.BonReception;
using CoperativeTelouet.Domain.Enums;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Achat.BonsReception;

public partial class BonsReceptionDetailViewModel : ViewModelBase
{
    private readonly IBonReceptionFournisseurService _bons;
    private readonly IBonCommandeFournisseurService _bonsCommande;
    private readonly IFournisseurService _fournisseurService;
    private readonly IUserDialogService _dialogs;
    private readonly IDocumentColumnVisibilityService _columnVisibility;
    private BonsReceptionViewModel? _host;
    private int? _editingId;
    private bool _syncingBonCommande;

    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _statutLabel = "(brouillon)";

    [ObservableProperty]
    private DateTime _dateDocument = DateTime.Today;

    [ObservableProperty]
    private ObservableCollection<BonCommandeFournisseurListItemDto> _bonCommandeOptions = [];

    [ObservableProperty]
    private BonCommandeFournisseurListItemDto? _selectedBonCommande;

    [ObservableProperty]
    private ObservableCollection<TiersDto> _fournisseurOptions = [];

    [ObservableProperty]
    private TiersDto? _selectedFournisseur;

    [ObservableProperty]
    private ObservableCollection<BonsReceptionLigneItemViewModel> _lignes = [];

    [ObservableProperty]
    private BonsReceptionLigneItemViewModel? _selectedLigne;

    [ObservableProperty]
    private string _articleSearch = string.Empty;

    [ObservableProperty]
    private ObservableCollection<ArticleSuggestionDto> _articleSuggestions = [];

    [ObservableProperty]
    private bool _showSuggestions;

    [ObservableProperty]
    private string? _note;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _showColRef = true;

    [ObservableProperty]
    private bool _showColDesignation = true;

    [ObservableProperty]
    private bool _showColQte = true;

    [ObservableProperty]
    private bool _showColPrix = true;

    [ObservableProperty]
    private bool _showColTva = true;

    [ObservableProperty]
    private bool _showColHt = true;

    [ObservableProperty]
    private bool _showColTtc = true;

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    public bool IsNew => _editingId is null;

    public decimal TotalHt
    {
        get
        {
            var (ht, _, _) = IBonReceptionFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return ht;
        }
    }

    public decimal TotalTva
    {
        get
        {
            var (_, tva, _) = IBonReceptionFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return tva;
        }
    }

    public decimal TotalTtc
    {
        get
        {
            var (_, _, ttc) = IBonReceptionFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return ttc;
        }
    }

    public BonsReceptionDetailViewModel(
        IBonReceptionFournisseurService bons,
        IBonCommandeFournisseurService bonsCommande,
        IFournisseurService fournisseurService,
        IUserDialogService dialogs,
        IDocumentColumnVisibilityService columnVisibility)
    {
        _bons = bons;
        _bonsCommande = bonsCommande;
        _fournisseurService = fournisseurService;
        _dialogs = dialogs;
        _columnVisibility = columnVisibility;
        Lignes.CollectionChanged += OnLignesCollectionChanged;
    }

    public void AttachHost(BonsReceptionViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnArticleSearchChanged(string value) => _ = SearchArticlesAsync();

    partial void OnSelectedBonCommandeChanged(BonCommandeFournisseurListItemDto? value)
    {
        if (_syncingBonCommande || value is null) return;
        SelectedFournisseur = FournisseurOptions.FirstOrDefault(f => f.Id == value.FournisseurId)
            ?? SelectedFournisseur;
    }

    [RelayCommand]
    public async Task LoadAsync(int? id)
    {
        _editingId = id;
        OnPropertyChanged(nameof(IsNew));
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            await ApplyColumnVisibilityAsync();

            var fournisseurs = await _fournisseurService.GetFournisseursAsync(page: 1, pageSize: 500);
            FournisseurOptions = new ObservableCollection<TiersDto>(
                fournisseurs.Items.Where(f => f.Type is TypeTiers.Fournisseur or TypeTiers.LesDeux).OrderBy(f => f.Nom));

            var bonsCommande = await _bonsCommande.GetBonsCommandeAsync(page: 1, pageSize: 500);
            BonCommandeOptions = new ObservableCollection<BonCommandeFournisseurListItemDto>(
                bonsCommande.Items.OrderByDescending(b => b.Date));

            _syncingBonCommande = true;
            try
            {
                if (id is null)
                {
                    Numero = await _bons.GenerateNumeroAsync();
                    StatutLabel = "(brouillon)";
                    DateDocument = DateTime.Today;
                    SelectedBonCommande = BonCommandeOptions.FirstOrDefault();
                    SelectedFournisseur = SelectedBonCommande is null
                        ? FournisseurOptions.FirstOrDefault()
                        : FournisseurOptions.FirstOrDefault(f => f.Id == SelectedBonCommande.FournisseurId)
                          ?? FournisseurOptions.FirstOrDefault();
                    Note = null;
                    ReplaceLignes([]);
                }
                else
                {
                    var dto = await _bons.GetBonReceptionByIdAsync(id.Value)
                        ?? throw new KeyNotFoundException("Bon de réception introuvable.");

                    Numero = dto.Numero;
                    StatutLabel = string.Empty;
                    DateDocument = dto.Date.Date;
                    SelectedBonCommande = BonCommandeOptions.FirstOrDefault(b => b.Id == dto.BonCommandeFournisseurId)
                        ?? BonCommandeOptions.FirstOrDefault();
                    SelectedFournisseur = FournisseurOptions.FirstOrDefault(f => f.Id == dto.FournisseurId)
                        ?? FournisseurOptions.FirstOrDefault();
                    Note = dto.Note;
                    ReplaceLignes(dto.Lignes.Select(BonsReceptionLigneItemViewModel.FromDto));
                }
            }
            finally
            {
                _syncingBonCommande = false;
            }

            NotifyTotals();
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
    private async Task SearchArticlesAsync()
    {
        try
        {
            var items = await _bons.SearchArticlesAsync(
                string.IsNullOrWhiteSpace(ArticleSearch) ? null : ArticleSearch);
            ArticleSuggestions = new ObservableCollection<ArticleSuggestionDto>(items);
            ShowSuggestions = ArticleSuggestions.Count > 0 && !string.IsNullOrWhiteSpace(ArticleSearch);
        }
        catch
        {
            ArticleSuggestions = [];
            ShowSuggestions = false;
        }
    }

    [RelayCommand]
    private void AddArticle(ArticleSuggestionDto? article)
    {
        if (article is null) return;
        var line = BonsReceptionLigneItemViewModel.FromArticle(article);
        line.PropertyChanged += OnLignePropertyChanged;
        Lignes.Add(line);
        ArticleSearch = string.Empty;
        ShowSuggestions = false;
        NotifyTotals();
    }

    [RelayCommand]
    private void RemoveSelectedLigne()
    {
        if (SelectedLigne is null) return;
        SelectedLigne.PropertyChanged -= OnLignePropertyChanged;
        Lignes.Remove(SelectedLigne);
        SelectedLigne = null;
        NotifyTotals();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            if (SelectedBonCommande is null)
                throw new InvalidOperationException("Le bon de commande est obligatoire.");
            if (SelectedFournisseur is null)
                throw new InvalidOperationException("Le fournisseur est obligatoire.");
            if (Lignes.Count == 0)
                throw new InvalidOperationException("Le bon de réception doit contenir au moins une ligne.");

            var lineDtos = Lignes.Select(l => l.ToCreateDto()).ToList();
            var (_, _, ttc) = IBonReceptionFournisseurService.ComputeTotals(lineDtos);

            if (_editingId is null)
            {
                await _bons.CreateBonReceptionAsync(new CreateBonReceptionFournisseurDto(
                    Numero,
                    SelectedBonCommande.Id,
                    SelectedFournisseur.Id,
                    SelectedBonCommande.DevisFournisseurId,
                    DateDocument.Date,
                    ttc,
                    string.IsNullOrWhiteSpace(Note) ? null : Note.Trim(),
                    lineDtos));
            }
            else
            {
                await _bons.UpdateBonReceptionAsync(_editingId.Value, new UpdateBonReceptionFournisseurDto(
                    SelectedBonCommande.Id,
                    SelectedFournisseur.Id,
                    SelectedBonCommande.DevisFournisseurId,
                    DateDocument.Date,
                    ttc,
                    string.IsNullOrWhiteSpace(Note) ? null : Note.Trim(),
                    lineDtos));
            }

            await SyncColumnVisibilityAsync();

            await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
            _host?.ShowList();
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
    private void RetourListe() => _host?.ShowList();

    private void ReplaceLignes(IEnumerable<BonsReceptionLigneItemViewModel> items)
    {
        foreach (var line in Lignes)
            line.PropertyChanged -= OnLignePropertyChanged;

        Lignes.Clear();
        foreach (var line in items)
        {
            line.PropertyChanged += OnLignePropertyChanged;
            Lignes.Add(line);
        }
    }

    private void OnLignesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => NotifyTotals();

    private void OnLignePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BonsReceptionLigneItemViewModel.MontantHt)
            or nameof(BonsReceptionLigneItemViewModel.MontantTtc)
            or nameof(BonsReceptionLigneItemViewModel.QuantiteRecue)
            or nameof(BonsReceptionLigneItemViewModel.PrixUnitaireHt)
            or nameof(BonsReceptionLigneItemViewModel.TauxTva))
            NotifyTotals();
    }

    private void NotifyTotals()
    {
        OnPropertyChanged(nameof(TotalHt));
        OnPropertyChanged(nameof(TotalTva));
        OnPropertyChanged(nameof(TotalTtc));
    }

    private async Task ApplyColumnVisibilityAsync()
    {
        var prefs = await _columnVisibility.GetAsync(DocumentColumnKeys.BonReceptionFournisseur);
        ShowColRef = prefs.ShowColRef;
        ShowColDesignation = prefs.ShowColDesignation;
        ShowColQte = prefs.ShowColQte;
        ShowColPrix = prefs.ShowColPrix;
        ShowColTva = prefs.ShowColTva;
        ShowColHt = prefs.ShowColHt;
        ShowColTtc = prefs.ShowColTtc;
    }

    private Task SyncColumnVisibilityAsync() =>
        _columnVisibility.SaveAsync(
            DocumentColumnKeys.BonReceptionFournisseur,
            new DocumentColumnVisibility
            {
                ShowColRef = ShowColRef,
                ShowColDesignation = ShowColDesignation,
                ShowColQte = ShowColQte,
                ShowColPrix = ShowColPrix,
                ShowColRemise = false,
                ShowColTva = ShowColTva,
                ShowColConditionnement = false,
                ShowColHt = ShowColHt,
                ShowColTtc = ShowColTtc,
            });
}
