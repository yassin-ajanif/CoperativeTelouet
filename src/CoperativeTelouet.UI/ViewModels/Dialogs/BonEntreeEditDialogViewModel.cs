using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CoperativeTelouet.UI.ViewModels.Dialogs;

public partial class BonEntreeEditDialogViewModel : ViewModelBase
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
    private string? _selectedChambre;

    [ObservableProperty]
    private string? _selectedVariete;

    [ObservableProperty]
    private string _numeroLot = string.Empty;

    [ObservableProperty]
    private string? _errorMessage;

    public ObservableCollection<string> EtatOptions { get; } = ["Plein", "Vide"];

    public ObservableCollection<string> ChambreOptions { get; } =
        ["CF-1 Nord", "CF-2 Sud", "CF-3 Est"];

    public ObservableCollection<string> VarieteOptions { get; } =
        ["Golden", "Gala", "Starking", "Granny Smith"];

    public bool IsPlein => string.Equals(EtatBac, "Plein", StringComparison.OrdinalIgnoreCase);

    public bool IsSaved { get; private set; }

    public Action? CloseAction { get; set; }

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public BonEntreeEditDialogViewModel(string suggestedNumero)
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
            SelectedChambre = null;
            SelectedVariete = null;
            NumeroLot = string.Empty;
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

        if (IsPlein)
        {
            if (string.IsNullOrWhiteSpace(SelectedChambre))
            {
                ErrorMessage = "La chambre est obligatoire pour un bac plein.";
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedVariete))
            {
                ErrorMessage = "La variété est obligatoire pour un bac plein.";
                return;
            }

            if (string.IsNullOrWhiteSpace(NumeroLot))
            {
                ErrorMessage = "Le n° de lot est obligatoire pour un bac plein.";
                return;
            }
        }

        DateEntreeParsed = date;
        NombreBacsParsed = nombreBacs;
        IsSaved = true;
        CloseAction?.Invoke();
    }

    public DateTime DateEntreeParsed { get; private set; }
    public int NombreBacsParsed { get; private set; }

    private static bool TryParseDate(string? text, out DateTime date)
    {
        date = default;
        if (string.IsNullOrWhiteSpace(text))
            return false;
        return DateTime.TryParse(text, out date);
    }
}
