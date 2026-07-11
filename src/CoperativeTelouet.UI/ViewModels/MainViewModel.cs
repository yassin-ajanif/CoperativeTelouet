using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.UI.ViewModels.Vente.Avoirs;
using CoperativeTelouet.UI.ViewModels.Vente.BonsCommande;
using CoperativeTelouet.UI.ViewModels.Vente.BonsLivraison;
using CoperativeTelouet.UI.ViewModels.Vente.Clients;
using CoperativeTelouet.UI.ViewModels.Vente.Devis;
using CoperativeTelouet.UI.ViewModels.Vente.Facturation;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _services;

    [ObservableProperty]
    private ViewModelBase _currentPage = null!;

    [ObservableProperty]
    private string _statusMessage = "Prêt";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAccueilSelected))]
    [NotifyPropertyChangedFor(nameof(IsClientsSelected))]
    [NotifyPropertyChangedFor(nameof(IsDevisSelected))]
    [NotifyPropertyChangedFor(nameof(IsBonsCommandeSelected))]
    [NotifyPropertyChangedFor(nameof(IsBonsLivraisonSelected))]
    [NotifyPropertyChangedFor(nameof(IsFacturationSelected))]
    [NotifyPropertyChangedFor(nameof(IsAvoirsSelected))]
    [NotifyPropertyChangedFor(nameof(IsCategoriesSelected))]
    private string _selectedNav = "Accueil";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(VenteChevron))]
    private bool _isVenteExpanded = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CatalogueChevron))]
    private bool _isCatalogueExpanded;

    public bool IsAccueilSelected => SelectedNav == "Accueil";
    public bool IsClientsSelected => SelectedNav == "Clients";
    public bool IsDevisSelected => SelectedNav == "Devis";
    public bool IsBonsCommandeSelected => SelectedNav == "BonsCommande";
    public bool IsBonsLivraisonSelected => SelectedNav == "BonsLivraison";
    public bool IsFacturationSelected => SelectedNav == "Facturation";
    public bool IsAvoirsSelected => SelectedNav == "Avoirs";
    public bool IsCategoriesSelected => SelectedNav == "Categories";

    public string VenteChevron => IsVenteExpanded ? "▼" : "▶";
    public string CatalogueChevron => IsCatalogueExpanded ? "▼" : "▶";

    public MainViewModel(IServiceProvider services)
    {
        _services = services;
        NavigateToAccueil();
    }

    [RelayCommand]
    private void ToggleVente() => IsVenteExpanded = !IsVenteExpanded;

    [RelayCommand]
    private void ToggleCatalogue() => IsCatalogueExpanded = !IsCatalogueExpanded;

    [RelayCommand]
    private void NavigateToAccueil()
    {
        CurrentPage = _services.GetRequiredService<AccueilViewModel>();
        SelectedNav = "Accueil";
        StatusMessage = "Accueil";
    }

    [RelayCommand]
    private void NavigateToClients()
    {
        CurrentPage = _services.GetRequiredService<ClientsViewModel>();
        SelectedNav = "Clients";
        StatusMessage = "Vente · Clients";
        IsVenteExpanded = true;
    }

    [RelayCommand]
    private void NavigateToDevis()
    {
        CurrentPage = _services.GetRequiredService<DevisViewModel>();
        SelectedNav = "Devis";
        StatusMessage = "Vente · Devis";
        IsVenteExpanded = true;
    }

    [RelayCommand]
    private void NavigateToBonsCommande()
    {
        CurrentPage = _services.GetRequiredService<BonsCommandeViewModel>();
        SelectedNav = "BonsCommande";
        StatusMessage = "Vente · Bons de commande";
        IsVenteExpanded = true;
    }

    [RelayCommand]
    private void NavigateToBonsLivraison()
    {
        CurrentPage = _services.GetRequiredService<BonsLivraisonViewModel>();
        SelectedNav = "BonsLivraison";
        StatusMessage = "Vente · Bons de livraison";
        IsVenteExpanded = true;
    }

    [RelayCommand]
    private void NavigateToFacturation()
    {
        CurrentPage = _services.GetRequiredService<FacturationViewModel>();
        SelectedNav = "Facturation";
        StatusMessage = "Vente · Facturation";
        IsVenteExpanded = true;
    }

    [RelayCommand]
    private void NavigateToAvoirs()
    {
        CurrentPage = _services.GetRequiredService<AvoirsViewModel>();
        SelectedNav = "Avoirs";
        StatusMessage = "Vente · Avoirs";
        IsVenteExpanded = true;
    }

    [RelayCommand]
    private void NavigateToCategories()
    {
        CurrentPage = _services.GetRequiredService<CategoriesViewModel>();
        SelectedNav = "Categories";
        StatusMessage = "Catalogue · Catégories";
        IsCatalogueExpanded = true;
    }
}
