using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.Services.Client;
using CoperativeTelouet.Domain.Enums;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Vente.Devis;

public partial class DevisDetailViewModel : ViewModelBase
{
    private readonly IDevisClientService _devis;
    private readonly IClientService _clientService;
    private readonly IUserDialogService _dialogs;
    private readonly IDocumentColumnVisibilityService _columnVisibility;
    private DevisViewModel? _host;
    private int? _editingId;

    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _statutLabel = "(brouillon)";

    [ObservableProperty]
    private DateTime _dateDevis = DateTime.Today;

    [ObservableProperty]
    private DateTime _dateValidite = DateTime.Today.AddMonths(1);

    [ObservableProperty]
    private ObservableCollection<TiersDto> _clientOptions = [];

    [ObservableProperty]
    private TiersDto? _selectedClient;

    [ObservableProperty]
    private ObservableCollection<DevisLigneItemViewModel> _lignes = [];

    [ObservableProperty]
    private DevisLigneItemViewModel? _selectedLigne;

    [ObservableProperty]
    private string _articleSearch = string.Empty;

    [ObservableProperty]
    private ObservableCollection<ArticleSuggestionDto> _articleSuggestions = [];

    [ObservableProperty]
    private bool _showSuggestions;

    [ObservableProperty]
    private decimal _remiseGlobale;

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
            var (ht, _, _) = IDevisClientService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()), RemiseGlobale);
            return ht;
        }
    }

    public decimal TotalTva
    {
        get
        {
            var (_, tva, _) = IDevisClientService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()), RemiseGlobale);
            return tva;
        }
    }

    public decimal TotalTtc
    {
        get
        {
            var (_, _, ttc) = IDevisClientService.ComputeTotals(Lignes.Select(l => l.ToCreateDto()), RemiseGlobale);
            return ttc;
        }
    }

    public DevisDetailViewModel(
        IDevisClientService devis,
        IClientService clientService,
        IUserDialogService dialogs,
        IDocumentColumnVisibilityService columnVisibility)
    {
        _devis = devis;
        _clientService = clientService;
        _dialogs = dialogs;
        _columnVisibility = columnVisibility;
        Lignes.CollectionChanged += OnLignesCollectionChanged;
    }

    public void AttachHost(DevisViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnRemiseGlobaleChanged(decimal value) => NotifyTotals();
    partial void OnArticleSearchChanged(string value) => _ = SearchArticlesAsync();

    [RelayCommand]
    public async Task LoadAsync(int? devisId)
    {
        _editingId = devisId;
        OnPropertyChanged(nameof(IsNew));
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            await ApplyColumnVisibilityAsync();

            var clients = await _clientService.GetClientsAsync(page: 1, pageSize: 500);
            ClientOptions = new ObservableCollection<TiersDto>(
                clients.Items.Where(c => c.Type is TypeTiers.Client or TypeTiers.LesDeux).OrderBy(c => c.Nom));

            if (devisId is null)
            {
                Numero = await _devis.GenerateNumeroAsync();
                StatutLabel = "(brouillon)";
                DateDevis = DateTime.Today;
                DateValidite = DateTime.Today.AddMonths(1);
                SelectedClient = ClientOptions.FirstOrDefault();
                RemiseGlobale = 0;
                Note = null;
                ReplaceLignes([]);
            }
            else
            {
                var dto = await _devis.GetDevisByIdAsync(devisId.Value)
                    ?? throw new KeyNotFoundException("Devis introuvable.");

                Numero = dto.Numero;
                StatutLabel = string.Empty;
                DateDevis = dto.Date.Date;
                DateValidite = dto.DateValidite.Date;
                SelectedClient = ClientOptions.FirstOrDefault(c => c.Id == dto.ClientId)
                    ?? ClientOptions.FirstOrDefault();
                RemiseGlobale = dto.RemiseGlobale;
                Note = dto.Note;
                ReplaceLignes(dto.Lignes.Select(DevisLigneItemViewModel.FromDto));
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
            var items = await _devis.SearchArticlesAsync(
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
        var line = DevisLigneItemViewModel.FromArticle(article);
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
                throw new InvalidOperationException("Le devis doit contenir au moins une ligne.");

            var lineDtos = Lignes.Select(l => l.ToCreateDto()).ToList();
            var (_, _, ttc) = IDevisClientService.ComputeTotals(lineDtos, RemiseGlobale);

            if (_editingId is null)
            {
                await _devis.CreateDevisAsync(new CreateDevisClientDto(
                    Numero,
                    SelectedClient.Id,
                    DateDevis.Date,
                    DateValidite.Date,
                    RemiseGlobale,
                    ttc,
                    string.IsNullOrWhiteSpace(Note) ? null : Note.Trim(),
                    lineDtos));
            }
            else
            {
                await _devis.UpdateDevisAsync(_editingId.Value, new UpdateDevisClientDto(
                    SelectedClient.Id,
                    DateDevis.Date,
                    DateValidite.Date,
                    RemiseGlobale,
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

    private void ReplaceLignes(IEnumerable<DevisLigneItemViewModel> items)
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
        if (e.PropertyName is nameof(DevisLigneItemViewModel.MontantHt)
            or nameof(DevisLigneItemViewModel.MontantTtc)
            or nameof(DevisLigneItemViewModel.Quantite)
            or nameof(DevisLigneItemViewModel.PrixUnitaireHt)
            or nameof(DevisLigneItemViewModel.Remise)
            or nameof(DevisLigneItemViewModel.TauxTva))
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
        var prefs = await _columnVisibility.GetAsync(DocumentColumnKeys.DevisClient);
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
            DocumentColumnKeys.DevisClient,
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
