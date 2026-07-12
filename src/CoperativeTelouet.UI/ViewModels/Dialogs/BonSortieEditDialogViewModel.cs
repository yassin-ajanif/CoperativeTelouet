using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CoperativeTelouet.UI.ViewModels.Dialogs;

public partial class BonSortieEditDialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _numero = string.Empty;

    [ObservableProperty]
    private string _clientNom = string.Empty;

    [ObservableProperty]
    private string _dateText = string.Empty;

    [ObservableProperty]
    private string _etatBac = "Plein";

    [ObservableProperty]
    private string _nombreBacsText = "1";

    [ObservableProperty]
    private string? _selectedBonEntree;

    [ObservableProperty]
    private string _factureNumero = string.Empty;

    [ObservableProperty]
    private string? _errorMessage;

    public ObservableCollection<string> EtatOptions { get; } = ["Plein", "Vide"];

    public ObservableCollection<string> BonEntreeOptions { get; } =
        ["BE-2026-12", "BE-2026-11", "BE-2026-10", "BE-2026-09", "BE-2026-05", "BE-2026-04"];

    public bool IsPlein => string.Equals(EtatBac, "Plein", StringComparison.OrdinalIgnoreCase);

    public bool IsSaved { get; private set; }

    public Action? CloseAction { get; set; }

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public DateTime DateSortieParsed { get; private set; }
    public int NombreBacsParsed { get; private set; }

    public BonSortieEditDialogViewModel(string suggestedNumero)
    {
        Numero = suggestedNumero;
        DateText = DateTime.Today.ToString("dd/MM/yyyy");
    }

    partial void OnEtatBacChanged(string value)
    {
        OnPropertyChanged(nameof(IsPlein));
        ErrorMessage = null;
        if (!IsPlein)
        {
            SelectedBonEntree = null;
            FactureNumero = string.Empty;
        }
    }

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));

    [RelayCommand]
    private void Annuler()
    {
        IsSaved = false;
        CloseAction?.Invoke();
    }

    [RelayCommand]
    private void Enregistrer()
    {
        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(Numero))
        {
            ErrorMessage = "Le numéro est obligatoire.";
            return;
        }

        if (string.IsNullOrWhiteSpace(ClientNom))
        {
            ErrorMessage = "Le client est obligatoire.";
            return;
        }

        if (!TryParseDate(DateText, out var date))
        {
            ErrorMessage = "Date invalide (jj/mm/aaaa).";
            return;
        }

        if (!int.TryParse(NombreBacsText, out var nombreBacs) || nombreBacs <= 0)
        {
            ErrorMessage = "Nombre de bacs invalide.";
            return;
        }

        if (IsPlein && string.IsNullOrWhiteSpace(SelectedBonEntree))
        {
            ErrorMessage = "Le bon d'entrée est obligatoire pour une sortie plein.";
            return;
        }

        DateSortieParsed = date;
        NombreBacsParsed = nombreBacs;
        IsSaved = true;
        CloseAction?.Invoke();
    }

    private static bool TryParseDate(string? text, out DateTime date)
    {
        date = default;
        if (string.IsNullOrWhiteSpace(text))
            return false;
        return DateTime.TryParse(text, out date);
    }
}
