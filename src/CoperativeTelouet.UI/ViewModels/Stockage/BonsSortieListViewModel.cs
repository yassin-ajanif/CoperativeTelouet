using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.UI.Services;
using CoperativeTelouet.UI.ViewModels.Dialogs;
using CoperativeTelouet.UI.Views.Dialogs;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class BonsSortieListViewModel : ViewModelBase
{
    private readonly IUserDialogService _dialogs;
    private readonly List<BonSortieListItemViewModel> _all = [];
    private int _nextNumero = 9;

    [ObservableProperty]
    private ObservableCollection<BonSortieListItemViewModel> _items = [];

    [ObservableProperty]
    private BonSortieListItemViewModel? _selectedItem;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _statusHint = "Données mock — Business à brancher plus tard.";

    public BonsSortieListViewModel(IUserDialogService dialogs)
    {
        _dialogs = dialogs;
        LoadMockData();
        ApplyFilter();
    }

    private void LoadMockData()
    {
        _all.Clear();
        _all.Add(new BonSortieListItemViewModel
        {
            Numero = "BS-2026-08",
            ClientNom = "Benali",
            DateSortie = new DateTime(2026, 7, 12),
            EtatBac = "Plein",
            NombreBacs = 20,
            BonEntreeNumero = "BE-2026-05",
            FactureNumero = "FC-2026-03"
        });
        _all.Add(new BonSortieListItemViewModel
        {
            Numero = "BS-2026-07",
            ClientNom = "Alami",
            DateSortie = new DateTime(2026, 7, 9),
            EtatBac = "Plein",
            NombreBacs = 30,
            BonEntreeNumero = "BE-2026-04",
            FactureNumero = "-"
        });
        _all.Add(new BonSortieListItemViewModel
        {
            Numero = "BS-2026-06",
            ClientNom = "Idri",
            DateSortie = new DateTime(2026, 7, 7),
            EtatBac = "Vide",
            NombreBacs = 10,
            BonEntreeNumero = "-",
            FactureNumero = "-"
        });
    }

    partial void OnSearchTextChanged(string value) => ApplyFilter();

    private void ApplyFilter()
    {
        var q = SearchText?.Trim() ?? string.Empty;
        IEnumerable<BonSortieListItemViewModel> query = _all;
        if (!string.IsNullOrWhiteSpace(q))
        {
            query = _all.Where(x =>
                x.Numero.Contains(q, StringComparison.OrdinalIgnoreCase)
                || x.ClientNom.Contains(q, StringComparison.OrdinalIgnoreCase)
                || x.BonEntreeNumero.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        Items = new ObservableCollection<BonSortieListItemViewModel>(
            query.OrderByDescending(x => x.DateSortie));
    }

    [RelayCommand]
    private async Task NouveauAsync()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow is null)
            return;

        var suggested = $"BS-2026-{_nextNumero:D2}";
        var vm = new BonSortieEditDialogViewModel(suggested);
        var dialog = new BonSortieEditDialogView { DataContext = vm };
        vm.CloseAction = () => dialog.Close();

        await dialog.ShowDialog(desktop.MainWindow);

        if (!vm.IsSaved)
            return;

        var isPlein = string.Equals(vm.EtatBac, "Plein", StringComparison.OrdinalIgnoreCase);
        var facture = vm.FactureNumero.Trim();
        var item = new BonSortieListItemViewModel
        {
            Numero = vm.Numero.Trim(),
            ClientNom = vm.ClientNom.Trim(),
            DateSortie = vm.DateSortieParsed.Date,
            EtatBac = isPlein ? "Plein" : "Vide",
            NombreBacs = vm.NombreBacsParsed,
            BonEntreeNumero = isPlein ? (vm.SelectedBonEntree ?? "-") : "-",
            FactureNumero = isPlein && !string.IsNullOrWhiteSpace(facture) ? facture : "-"
        };

        _all.Add(item);
        _nextNumero++;
        ApplyFilter();
        SelectedItem = item;
        StatusHint = $"Bon de sortie {item.Numero} ajouté (mock).";
        await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
    }

    [RelayCommand]
    private void Actualiser()
    {
        ApplyFilter();
        StatusHint = "Liste actualisée (mock).";
    }
}
