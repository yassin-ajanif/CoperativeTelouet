using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur;
using CoperativeTelouet.Business.Services.Fournisseur.Avoir;
using CoperativeTelouet.Business.Services.Fournisseur.Facture;
using CoperativeTelouet.Domain.Enums;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Achat.Avoirs;

public partial class AvoirFournisseurDetailViewModel : ViewModelBase
{
    private readonly IAvoirFournisseurService _avoirs;
    private readonly IFactureFournisseurService _factures;
    private readonly IFournisseurService _fournisseurService;
    private readonly IUserDialogService _dialogs;
    private readonly IDocumentColumnVisibilityService _columnVisibility;
    private AvoirFournisseurViewModel? _host;
    private int? _editingId;
    private bool _syncingFacture;

    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _statutLabel = "(brouillon)";

    [ObservableProperty]
    private DateTime _dateDocument = DateTime.Today;

    [ObservableProperty]
    private ObservableCollection<FactureFournisseurListItemDto> _factureOptions = [];

    [ObservableProperty]
    private FactureFournisseurListItemDto? _selectedFacture;

    [ObservableProperty]
    private ObservableCollection<TiersDto> _fournisseurOptions = [];

    [ObservableProperty]
    private TiersDto? _selectedFournisseur;

    [ObservableProperty]
    private ObservableCollection<AvoirFournisseurLigneItemViewModel> _lignes = [];

    [ObservableProperty]
    private AvoirFournisseurLigneItemViewModel? _selectedLigne;

    [ObservableProperty]
    private string _articleSearch = string.Empty;

    [ObservableProperty]
    private ObservableCollection<ArticleSuggestionDto> _articleSuggestions = [];

    [ObservableProperty]
    private bool _showSuggestions;

    [ObservableProperty]
    private string? _motif;

    [ObservableProperty]
    private bool _retourMarchandise;

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
    private bool _showColRemise = true;

    [ObservableProperty]
    private bool _showColTva = true;

    [ObservableProperty]
    private bool _showColConditionnement;

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
            var (ht, _, _) = IAvoirFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return ht;
        }
    }

    public decimal TotalTva
    {
        get
        {
            var (_, tva, _) = IAvoirFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return tva;
        }
    }

    public decimal TotalTtc
    {
        get
        {
            var (_, _, ttc) = IAvoirFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return ttc;
        }
    }

    public AvoirFournisseurDetailViewModel(
        IAvoirFournisseurService avoirs,
        IFactureFournisseurService factures,
        IFournisseurService fournisseurService,
        IUserDialogService dialogs,
        IDocumentColumnVisibilityService columnVisibility)
    {
        _avoirs = avoirs;
        _factures = factures;
        _fournisseurService = fournisseurService;
        _dialogs = dialogs;
        _columnVisibility = columnVisibility;
        Lignes.CollectionChanged += OnLignesCollectionChanged;
    }

    public void AttachHost(AvoirFournisseurViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnArticleSearchChanged(string value) => _ = SearchArticlesAsync();

    partial void OnSelectedFactureChanged(FactureFournisseurListItemDto? value)
    {
        if (_syncingFacture || value is null) return;
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

            var factures = await _factures.GetFacturesAsync(page: 1, pageSize: 500);
            FactureOptions = new ObservableCollection<FactureFournisseurListItemDto>(
                factures.Items.OrderByDescending(f => f.Date));

            _syncingFacture = true;
            try
            {
                if (id is null)
                {
                    Numero = await _avoirs.GenerateNumeroAsync();
                    StatutLabel = "(brouillon)";
                    DateDocument = DateTime.Today;
                    SelectedFacture = FactureOptions.FirstOrDefault();
                    SelectedFournisseur = SelectedFacture is null
                        ? FournisseurOptions.FirstOrDefault()
                        : FournisseurOptions.FirstOrDefault(f => f.Id == SelectedFacture.FournisseurId)
                          ?? FournisseurOptions.FirstOrDefault();
                    Motif = null;
                    RetourMarchandise = false;
                    ReplaceLignes([]);
                }
                else
                {
                    var dto = await _avoirs.GetAvoirByIdAsync(id.Value)
                        ?? throw new KeyNotFoundException("Avoir introuvable.");

                    Numero = dto.Numero;
                    StatutLabel = string.Empty;
                    DateDocument = dto.Date.Date;
                    SelectedFacture = FactureOptions.FirstOrDefault(f => f.Id == dto.FactureFournisseurId)
                        ?? FactureOptions.FirstOrDefault();
                    SelectedFournisseur = FournisseurOptions.FirstOrDefault(f => f.Id == dto.FournisseurId)
                        ?? FournisseurOptions.FirstOrDefault();
                    Motif = dto.Motif;
                    RetourMarchandise = dto.RetourMarchandise;
                    ReplaceLignes(dto.Lignes.Select(AvoirFournisseurLigneItemViewModel.FromDto));
                }
            }
            finally
            {
                _syncingFacture = false;
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
            var items = await _avoirs.SearchArticlesAsync(
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

        var existing = FindExistingLigne(article);
        if (existing is not null)
        {
            existing.Quantite += 1;
            SelectedLigne = existing;
        }
        else
        {
            var line = AvoirFournisseurLigneItemViewModel.FromArticle(article);
            line.PropertyChanged += OnLignePropertyChanged;
            Lignes.Add(line);
            SelectedLigne = line;
        }

        ArticleSearch = string.Empty;
        ShowSuggestions = false;
        NotifyTotals();
    }

    private AvoirFournisseurLigneItemViewModel? FindExistingLigne(ArticleSuggestionDto article) =>
        Lignes.FirstOrDefault(l =>
            (article.ProduitId is not null && l.ProduitId == article.ProduitId)
            || (article.ServiceId is not null && l.ServiceId == article.ServiceId));

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

            if (SelectedFacture is null)
                throw new InvalidOperationException("La facture est obligatoire.");
            if (SelectedFournisseur is null)
                throw new InvalidOperationException("Le fournisseur est obligatoire.");
            if (Lignes.Count == 0)
                throw new InvalidOperationException("L'avoir doit contenir au moins une ligne.");

            var lineDtos = Lignes.Select(l => l.ToCreateDto()).ToList();
            var (_, _, ttc) = IAvoirFournisseurService.ComputeTotals(lineDtos);

            if (_editingId is null)
            {
                await _avoirs.CreateAvoirAsync(new CreateAvoirFournisseurDto(
                    Numero,
                    SelectedFacture.Id,
                    SelectedFournisseur.Id,
                    DateDocument.Date,
                    ttc,
                    string.IsNullOrWhiteSpace(Motif) ? null : Motif.Trim(),
                    RetourMarchandise,
                    lineDtos));
            }
            else
            {
                await _avoirs.UpdateAvoirAsync(_editingId.Value, new UpdateAvoirFournisseurDto(
                    SelectedFacture.Id,
                    SelectedFournisseur.Id,
                    DateDocument.Date,
                    ttc,
                    string.IsNullOrWhiteSpace(Motif) ? null : Motif.Trim(),
                    RetourMarchandise,
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

    private void ReplaceLignes(IEnumerable<AvoirFournisseurLigneItemViewModel> items)
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
        if (e.PropertyName is nameof(AvoirFournisseurLigneItemViewModel.MontantHt)
            or nameof(AvoirFournisseurLigneItemViewModel.MontantTtc)
            or nameof(AvoirFournisseurLigneItemViewModel.Quantite)
            or nameof(AvoirFournisseurLigneItemViewModel.PrixUnitaireHt)
            or nameof(AvoirFournisseurLigneItemViewModel.Remise)
            or nameof(AvoirFournisseurLigneItemViewModel.TauxTva))
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
        var prefs = await _columnVisibility.GetAsync(DocumentColumnKeys.AvoirFournisseur);
        ShowColRef = prefs.ShowColRef;
        ShowColDesignation = prefs.ShowColDesignation;
        ShowColQte = prefs.ShowColQte;
        ShowColPrix = prefs.ShowColPrix;
        ShowColRemise = prefs.ShowColRemise;
        ShowColTva = prefs.ShowColTva;
        ShowColConditionnement = prefs.ShowColConditionnement;
        ShowColHt = prefs.ShowColHt;
        ShowColTtc = prefs.ShowColTtc;
    }

    private Task SyncColumnVisibilityAsync() =>
        _columnVisibility.SaveAsync(
            DocumentColumnKeys.AvoirFournisseur,
            new DocumentColumnVisibility
            {
                ShowColRef = ShowColRef,
                ShowColDesignation = ShowColDesignation,
                ShowColQte = ShowColQte,
                ShowColPrix = ShowColPrix,
                ShowColRemise = ShowColRemise,
                ShowColTva = ShowColTva,
                ShowColConditionnement = ShowColConditionnement,
                ShowColHt = ShowColHt,
                ShowColTtc = ShowColTtc,
            });
}
