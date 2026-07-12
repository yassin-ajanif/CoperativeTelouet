using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CoperativeTelouet.UI.ViewModels.Stockage;

public partial class VarietesPommeViewModel : ViewModelBase
{
    private int _nextId = 5;

    [ObservableProperty]
    private ObservableCollection<VarietePommeItemViewModel> _items = [];

    [ObservableProperty]
    private VarietePommeItemViewModel? _selectedItem;

    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private string _statusHint = "Données mock — Business à brancher plus tard.";

    public VarietesPommeViewModel()
    {
        LoadMockData();
    }

    private void LoadMockData()
    {
        Items = new ObservableCollection<VarietePommeItemViewModel>
        {
            new() { Id = 1, Nom = "Golden" },
            new() { Id = 2, Nom = "Gala" },
            new() { Id = 3, Nom = "Starking" },
            new() { Id = 4, Nom = "Granny Smith" },
        };
    }

    partial void OnSelectedItemChanged(VarietePommeItemViewModel? value) =>
        Nom = value?.Nom ?? string.Empty;

    [RelayCommand]
    private void Nouveau()
    {
        SelectedItem = null;
        Nom = string.Empty;
        StatusHint = "Nouvelle variété — saisir puis Enregistrer (mock).";
    }

    [RelayCommand]
    private void Enregistrer()
    {
        if (string.IsNullOrWhiteSpace(Nom))
        {
            StatusHint = "Le nom est obligatoire.";
            return;
        }

        if (SelectedItem is null)
        {
            var item = new VarietePommeItemViewModel
            {
                Id = _nextId++,
                Nom = Nom.Trim()
            };
            Items.Add(item);
            SelectedItem = item;
            StatusHint = "Variété ajoutée (mock).";
        }
        else
        {
            SelectedItem.Nom = Nom.Trim();
            StatusHint = "Variété mise à jour (mock).";
        }
    }

    [RelayCommand]
    private void Supprimer()
    {
        if (SelectedItem is null)
        {
            StatusHint = "Sélectionnez une variété.";
            return;
        }

        Items.Remove(SelectedItem);
        SelectedItem = null;
        Nom = string.Empty;
        StatusHint = "Variété supprimée (mock).";
    }
}
