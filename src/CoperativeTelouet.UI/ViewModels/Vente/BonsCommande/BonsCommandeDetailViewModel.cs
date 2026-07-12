using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.Services.Client;
using CoperativeTelouet.Business.Services.Client.BonCommande;
using CoperativeTelouet.Domain.Enums;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Vente.BonsCommande;

public partial class BonsCommandeDetailViewModel : ViewModelBase
{
    private readonly IBonCommandeClientService _bons;
    private readonly IClientService _clientService;
    private readonly IUserDialogService _dialogs;
    private readonly IDocumentColumnVisibilityService _columnVisibility;
    private BonsCommandeViewModel? _host;
    private int? _editingId;

    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _statutLabel = "(brouillon)";

    [ObservableProperty]
    private DateTime _dateDocument = DateTime.Today;

    [ObservableProperty]
    private ObservableCollection<TiersDto> _clientOptions = [];

    [ObservableProperty]
    private TiersDto? _selectedClient;

    [ObservableProperty]
    private ObservableCollection<BonCommandeLigneItemViewModel> _lignes = [];

    [ObservableProperty]
    private BonCommandeLigneItemViewModel? _selectedLigne;

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
            var (ht, _, _) = DocumentTotals.Compute(
                Lignes.Select(l => (l.Quantite, l.PrixUnitaireHt, l.Remise, l.TauxTva)));
            return ht;
        }
    }

    public decimal TotalTva
    {
        get
        {
            var (_, tva, _) = DocumentTotals.Compute(
                Lignes.Select(l => (l.Quantite, l.PrixUnitaireHt, l.Remise, l.TauxTva)));
            return tva;
        }
    }

    public decimal TotalTtc
    {
        get
        {
            var (_, _, ttc) = DocumentTotals.Compute(
                Lignes.Select(l => (l.Quantite, l.PrixUnitaireHt, l.Remise, l.TauxTva)));
            return ttc;
        }
    }

    public BonsCommandeDetailViewModel(
        IBonCommandeClientService bons,
        IClientService clientService,
        IUserDialogService dialogs,
        IDocumentColumnVisibilityService columnVisibility)
    {
        _bons = bons;
        _clientService = clientService;
        _dialogs = dialogs;
        _columnVisibility = columnVisibility;
        Lignes.CollectionChanged += OnLignesCollectionChanged;
    }

    public void AttachHost(BonsCommandeViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnArticleSearchChanged(string value) => _ = SearchArticlesAsync();

    [RelayCommand]
    public async Task LoadAsync(int? bonCommandeId)
    {
        _editingId = bonCommandeId;
        OnPropertyChanged(nameof(IsNew));
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            await ApplyColumnVisibilityAsync();

            var clients = await _clientService.GetClientsAsync(page: 1, pageSize: 500);
            ClientOptions = new ObservableCollection<TiersDto>(
                clients.Items.Where(c => c.Type is TypeTiers.Client or TypeTiers.LesDeux).OrderBy(c => c.Nom));

            if (bonCommandeId is null)
            {
                Numero = await _bons.GenerateNumeroAsync();
                StatutLabel = "(brouillon)";
                DateDocument = DateTime.Today;
                SelectedClient = ClientOptions.FirstOrDefault();
                Note = null;
                ReplaceLignes([]);
            }
            else
            {
                var dto = await _bons.GetBonCommandeByIdAsync(bonCommandeId.Value)
                    ?? throw new KeyNotFoundException("Bon de commande introuvable.");

                Numero = dto.Numero;
                StatutLabel = string.Empty;
                DateDocument = dto.Date.Date;
                SelectedClient = ClientOptions.FirstOrDefault(c => c.Id == dto.ClientId)
                    ?? ClientOptions.FirstOrDefault();
                Note = dto.Note;
                ReplaceLignes(dto.Lignes.Select(BonCommandeLigneItemViewModel.FromDto));
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
        var line = BonCommandeLigneItemViewModel.FromArticle(article);
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

            if (SelectedClient is null)
                throw new InvalidOperationException("Le client est obligatoire.");
            if (Lignes.Count == 0)
                throw new InvalidOperationException("Le bon de commande doit contenir au moins une ligne.");

            var lineDtos = Lignes.Select(l => l.ToCreateDto()).ToList();
            var (_, _, ttc) = DocumentTotals.Compute(
                lineDtos.Select(l => (l.QuantiteCommandee, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));

            if (_editingId is null)
            {
                await _bons.CreateBonCommandeAsync(new CreateBonCommandeClientDto(
                    Numero,
                    SelectedClient.Id,
                    null,
                    DateDocument.Date,
                    ttc,
                    string.IsNullOrWhiteSpace(Note) ? null : Note.Trim(),
                    lineDtos));
            }
            else
            {
                await _bons.UpdateBonCommandeAsync(_editingId.Value, new UpdateBonCommandeClientDto(
                    SelectedClient.Id,
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

    private void ReplaceLignes(IEnumerable<BonCommandeLigneItemViewModel> items)
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
        if (e.PropertyName is nameof(BonCommandeLigneItemViewModel.MontantHt)
            or nameof(BonCommandeLigneItemViewModel.MontantTtc)
            or nameof(BonCommandeLigneItemViewModel.Quantite)
            or nameof(BonCommandeLigneItemViewModel.PrixUnitaireHt)
            or nameof(BonCommandeLigneItemViewModel.Remise)
            or nameof(BonCommandeLigneItemViewModel.TauxTva))
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
        var prefs = await _columnVisibility.GetAsync(DocumentColumnKeys.BonCommandeClient);
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
            DocumentColumnKeys.BonCommandeClient,
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
