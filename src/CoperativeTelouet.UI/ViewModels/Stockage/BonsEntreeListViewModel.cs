using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.UI.Services;
using CoperativeTelouet.UI.ViewModels.Dialogs;
using CoperativeTelouet.UI.Views.Dialogs;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class BonsEntreeListViewModel : ViewModelBase
{
    private readonly IUserDialogService _dialogs;
    private readonly List<BonEntreeListItemViewModel> _all = [];
    private int _nextNumero = 13;

    [ObservableProperty]
    private ObservableCollection<BonEntreeListItemViewModel> _items = [];

    [ObservableProperty]
    private BonEntreeListItemViewModel? _selectedItem;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _statusHint = "Données mock — Business à brancher plus tard.";

    public BonsEntreeListViewModel(IUserDialogService dialogs)
    {
        _dialogs = dialogs;
        LoadMockData();
        ApplyFilter();
    }

    private void LoadMockData()
    {
        _all.Clear();
        _all.Add(new BonEntreeListItemViewModel
        {
            Numero = "BE-2026-12",
            ClientNom = "Alami",
            DateEntree = new DateTime(2026, 7, 12),
            EtatBac = "Plein",
            NombreBacs = 40,
            ChambreNom = "CF-1 Nord",
            VarieteNom = "Golden",
            NumeroLot = "L-042"
        });
        _all.Add(new BonEntreeListItemViewModel
        {
            Numero = "BE-2026-11",
            ClientNom = "Idri",
            DateEntree = new DateTime(2026, 7, 11),
            EtatBac = "Vide",
            NombreBacs = 15,
            ChambreNom = "-",
            VarieteNom = "-",
            NumeroLot = "-"
        });
        _all.Add(new BonEntreeListItemViewModel
        {
            Numero = "BE-2026-10",
            ClientNom = "Benali",
            DateEntree = new DateTime(2026, 7, 10),
            EtatBac = "Plein",
            NombreBacs = 25,
            ChambreNom = "CF-2 Sud",
            VarieteNom = "Gala",
            NumeroLot = "L-043"
        });
        _all.Add(new BonEntreeListItemViewModel
        {
            Numero = "BE-2026-09",
            ClientNom = "Alami",
            DateEntree = new DateTime(2026, 7, 8),
            EtatBac = "Plein",
            NombreBacs = 55,
            ChambreNom = "CF-1 Nord",
            VarieteNom = "Starking",
            NumeroLot = "L-044"
        });
    }

    partial void OnSearchTextChanged(string value) => ApplyFilter();

    private void ApplyFilter()
    {
        var q = SearchText?.Trim() ?? string.Empty;
        IEnumerable<BonEntreeListItemViewModel> query = _all;
        if (!string.IsNullOrWhiteSpace(q))
        {
            query = _all.Where(x =>
                x.Numero.Contains(q, StringComparison.OrdinalIgnoreCase)
                || x.ClientNom.Contains(q, StringComparison.OrdinalIgnoreCase)
                || x.NumeroLot.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        Items = new ObservableCollection<BonEntreeListItemViewModel>(
            query.OrderByDescending(x => x.DateEntree));
    }

    [RelayCommand]
    private async Task NouveauAsync()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow is null)
            return;

        var suggested = $"BE-2026-{_nextNumero:D2}";
        var vm = new BonEntreeEditDialogViewModel(suggested);
        var dialog = new BonEntreeEditDialogView { DataContext = vm };
        vm.CloseAction = () => dialog.Close();

        await dialog.ShowDialog(desktop.MainWindow);

        if (!vm.IsSaved)
            return;

        var isPlein = string.Equals(vm.EtatBac, "Plein", StringComparison.OrdinalIgnoreCase);
        var item = new BonEntreeListItemViewModel
        {
            Numero = vm.Numero.Trim(),
            ClientNom = vm.ClientNom.Trim(),
            DateEntree = vm.DateEntreeParsed.Date,
            EtatBac = isPlein ? "Plein" : "Vide",
            NombreBacs = vm.NombreBacsParsed,
            ChambreNom = isPlein ? (vm.SelectedChambre ?? "-") : "-",
            VarieteNom = isPlein ? (vm.SelectedVariete ?? "-") : "-",
            NumeroLot = isPlein ? vm.NumeroLot.Trim() : "-"
        };

        _all.Add(item);
        _nextNumero++;
        ApplyFilter();
        SelectedItem = item;
        StatusHint = $"Bon d'entrée {item.Numero} ajouté (mock).";
        await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
    }

    [RelayCommand]
    private void Actualiser()
    {
        ApplyFilter();
        StatusHint = "Liste actualisée (mock).";
    }
}
