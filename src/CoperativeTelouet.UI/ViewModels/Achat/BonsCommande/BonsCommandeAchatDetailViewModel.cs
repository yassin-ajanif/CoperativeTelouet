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
using CoperativeTelouet.Domain.Enums;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Achat.BonsCommande;

public partial class BonsCommandeAchatDetailViewModel : ViewModelBase
{
    private readonly IBonCommandeFournisseurService _bons;
    private readonly IFournisseurService _fournisseurService;
    private readonly IUserDialogService _dialogs;
    private readonly IDocumentColumnVisibilityService _columnVisibility;
    private BonsCommandeAchatViewModel? _host;
    private int? _editingId;

    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _statutLabel = "(brouillon)";

    [ObservableProperty]
    private DateTime _dateDocument = DateTime.Today;

    [ObservableProperty]
    private ObservableCollection<TiersDto> _fournisseurOptions = [];

    [ObservableProperty]
    private TiersDto? _selectedFournisseur;

    [ObservableProperty]
    private ObservableCollection<BonsCommandeAchatLigneItemViewModel> _lignes = [];

    [ObservableProperty]
    private BonsCommandeAchatLigneItemViewModel? _selectedLigne;

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
            var (ht, _, _) = IBonCommandeFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return ht;
        }
    }

    public decimal TotalTva
    {
        get
        {
            var (_, tva, _) = IBonCommandeFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return tva;
        }
    }

    public decimal TotalTtc
    {
        get
        {
            var (_, _, ttc) = IBonCommandeFournisseurService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()));
            return ttc;
        }
    }

    public BonsCommandeAchatDetailViewModel(
        IBonCommandeFournisseurService bons,
        IFournisseurService fournisseurService,
        IUserDialogService dialogs,
        IDocumentColumnVisibilityService columnVisibility)
    {
        _bons = bons;
        _fournisseurService = fournisseurService;
        _dialogs = dialogs;
        _columnVisibility = columnVisibility;
        Lignes.CollectionChanged += OnLignesCollectionChanged;
    }

    public void AttachHost(BonsCommandeAchatViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnArticleSearchChanged(string value) => _ = SearchArticlesAsync();

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

            if (id is null)
            {
                Numero = await _bons.GenerateNumeroAsync();
                StatutLabel = "(brouillon)";
                DateDocument = DateTime.Today;
                SelectedFournisseur = FournisseurOptions.FirstOrDefault();
                Note = null;
                ReplaceLignes([]);
            }
            else
            {
                var dto = await _bons.GetBonCommandeByIdAsync(id.Value)
                    ?? throw new KeyNotFoundException("Bon de commande introuvable.");

                Numero = dto.Numero;
                StatutLabel = string.Empty;
                DateDocument = dto.Date.Date;
                SelectedFournisseur = FournisseurOptions.FirstOrDefault(f => f.Id == dto.FournisseurId)
                    ?? FournisseurOptions.FirstOrDefault();
                Note = dto.Note;
                ReplaceLignes(dto.Lignes.Select(BonsCommandeAchatLigneItemViewModel.FromDto));
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

        var existing = FindExistingLigne(article);
        if (existing is not null)
        {
            existing.QuantiteCommandee += 1;
            SelectedLigne = existing;
        }
        else
        {
            var line = BonsCommandeAchatLigneItemViewModel.FromArticle(article);
            line.PropertyChanged += OnLignePropertyChanged;
            Lignes.Add(line);
            SelectedLigne = line;
        }

        ArticleSearch = string.Empty;
        ShowSuggestions = false;
        NotifyTotals();
    }

    private BonsCommandeAchatLigneItemViewModel? FindExistingLigne(ArticleSuggestionDto article) =>
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

            if (SelectedFournisseur is null)
                throw new InvalidOperationException("Le fournisseur est obligatoire.");
            if (Lignes.Count == 0)
                throw new InvalidOperationException("Le bon de commande doit contenir au moins une ligne.");

            var lineDtos = Lignes.Select(l => l.ToCreateDto()).ToList();
            var (_, _, ttc) = IBonCommandeFournisseurService.ComputeTotals(lineDtos);

            if (_editingId is null)
            {
                await _bons.CreateBonCommandeAsync(new CreateBonCommandeFournisseurDto(
                    Numero,
                    SelectedFournisseur.Id,
                    null,
                    DateDocument.Date,
                    ttc,
                    string.IsNullOrWhiteSpace(Note) ? null : Note.Trim(),
                    lineDtos));
            }
            else
            {
                await _bons.UpdateBonCommandeAsync(_editingId.Value, new UpdateBonCommandeFournisseurDto(
                    SelectedFournisseur.Id,
                    null,
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

    private void ReplaceLignes(IEnumerable<BonsCommandeAchatLigneItemViewModel> items)
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
        if (e.PropertyName is nameof(BonsCommandeAchatLigneItemViewModel.MontantHt)
            or nameof(BonsCommandeAchatLigneItemViewModel.MontantTtc)
            or nameof(BonsCommandeAchatLigneItemViewModel.QuantiteCommandee)
            or nameof(BonsCommandeAchatLigneItemViewModel.PrixUnitaireHt)
            or nameof(BonsCommandeAchatLigneItemViewModel.Remise)
            or nameof(BonsCommandeAchatLigneItemViewModel.TauxTva))
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
        var prefs = await _columnVisibility.GetAsync(DocumentColumnKeys.BonCommandeFournisseur);
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
            DocumentColumnKeys.BonCommandeFournisseur,
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
