using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class ChambresFroidesViewModel : ViewModelBase
{
    private int _nextId = 4;

    [ObservableProperty]
    private ObservableCollection<ChambreFroideItemViewModel> _items = [];

    [ObservableProperty]
    private ChambreFroideItemViewModel? _selectedItem;

    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private string _capaciteText = "200";

    [ObservableProperty]
    private bool _actif = true;

    [ObservableProperty]
    private string _statusHint = "Données mock — Business à brancher plus tard.";

    public ChambresFroidesViewModel()
    {
        LoadMockData();
    }

    private void LoadMockData()
    {
        Items = new ObservableCollection<ChambreFroideItemViewModel>
        {
            new() { Id = 1, Nom = "CF-1 Nord", CapaciteBacs = 200, Actif = true },
            new() { Id = 2, Nom = "CF-2 Sud", CapaciteBacs = 200, Actif = true },
            new() { Id = 3, Nom = "CF-3 Est", CapaciteBacs = 200, Actif = true },
        };
    }

    partial void OnSelectedItemChanged(ChambreFroideItemViewModel? value)
    {
        Nom = value?.Nom ?? string.Empty;
        CapaciteText = value?.CapaciteBacs.ToString() ?? "200";
        Actif = value?.Actif ?? true;
    }

    [RelayCommand]
    private void Nouveau()
    {
        SelectedItem = null;
        Nom = string.Empty;
        CapaciteText = "200";
        Actif = true;
        StatusHint = "Nouvelle chambre — saisir puis Enregistrer (mock).";
    }

    [RelayCommand]
    private void Enregistrer()
    {
        if (string.IsNullOrWhiteSpace(Nom))
        {
            StatusHint = "Le nom est obligatoire.";
            return;
        }

        if (!int.TryParse(CapaciteText, out var capacite) || capacite <= 0)
        {
            StatusHint = "Capacité invalide.";
            return;
        }

        if (SelectedItem is null)
        {
            var item = new ChambreFroideItemViewModel
            {
                Id = _nextId++,
                Nom = Nom.Trim(),
                CapaciteBacs = capacite,
                Actif = Actif
            };
            Items.Add(item);
            SelectedItem = item;
            StatusHint = "Chambre ajoutée (mock).";
        }
        else
        {
            SelectedItem.Nom = Nom.Trim();
            SelectedItem.CapaciteBacs = capacite;
            SelectedItem.Actif = Actif;
            StatusHint = "Chambre mise à jour (mock).";
        }
    }

    [RelayCommand]
    private void Supprimer()
    {
        if (SelectedItem is null)
        {
            StatusHint = "Sélectionnez une chambre.";
            return;
        }

        Items.Remove(SelectedItem);
        SelectedItem = null;
        Nom = string.Empty;
        CapaciteText = "200";
        Actif = true;
        StatusHint = "Chambre supprimée (mock).";
    }
}
