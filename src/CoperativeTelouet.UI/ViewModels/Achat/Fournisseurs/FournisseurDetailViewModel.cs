using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services.Fournisseur;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Achat.Fournisseurs;

public partial class FournisseurDetailViewModel : ViewModelBase
{
    private readonly IFournisseurService _fournisseurs;
    private readonly IUserDialogService _dialogs;
    private FournisseursViewModel? _host;
    private int? _editingId;

    [ObservableProperty]
    private TypeTiersOption _selectedType = TypeTiersOption.Fournisseur;

    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private string? _ice;

    [ObservableProperty]
    private string? _adresse;

    [ObservableProperty]
    private string? _ville;

    [ObservableProperty]
    private string? _telephone;

    [ObservableProperty]
    private string? _email;

    [ObservableProperty]
    private string? _conditionsPaiement;

    [ObservableProperty]
    private bool _actif = true;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private decimal _soldeActuel;

    [ObservableProperty]
    private ObservableCollection<FournisseurCompteLigneDto> _compteLignes = [];

    [ObservableProperty]
    private bool _showCompte;

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    public string SoldeLabel => $"Solde actuel : {SoldeActuel:N2} DH";
    public IReadOnlyList<TypeTiersOption> TypeOptions { get; } =
    [
        TypeTiersOption.Fournisseur,
        TypeTiersOption.LesDeux,
    ];

    public bool IsNew => _editingId is null;

    public FournisseurDetailViewModel(IFournisseurService fournisseurs, IUserDialogService dialogs)
    {
        _fournisseurs = fournisseurs;
        _dialogs = dialogs;
    }

    public void AttachHost(FournisseursViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnSoldeActuelChanged(decimal value) => OnPropertyChanged(nameof(SoldeLabel));

    public async Task LoadAsync(int? fournisseurId)
    {
        _editingId = fournisseurId;
        OnPropertyChanged(nameof(IsNew));
        ErrorMessage = null;
        CompteLignes = [];
        ShowCompte = fournisseurId is not null;

        if (fournisseurId is null)
        {
            SelectedType = TypeTiersOption.Fournisseur;
            Nom = string.Empty;
            Ice = null;
            Adresse = null;
            Ville = null;
            Telephone = null;
            Email = null;
            ConditionsPaiement = null;
            Actif = true;
            SoldeActuel = 0;
            return;
        }

        try
        {
            IsBusy = true;
            var dto = await _fournisseurs.GetByIdAsync(fournisseurId.Value)
                ?? throw new KeyNotFoundException("Fournisseur introuvable.");

            SelectedType = dto.Type == Domain.Enums.TypeTiers.LesDeux
                ? TypeTiersOption.LesDeux
                : TypeTiersOption.Fournisseur;
            Nom = dto.Nom;
            Ice = dto.ICE;
            Adresse = dto.Adresse;
            Ville = dto.Ville;
            Telephone = dto.Telephone;
            Email = dto.Email;
            ConditionsPaiement = dto.ConditionsPaiement;
            Actif = dto.Actif;

            var compte = await _fournisseurs.GetCompteAsync(fournisseurId.Value);
            SoldeActuel = compte.SoldeActuel;
            CompteLignes = new ObservableCollection<FournisseurCompteLigneDto>(compte.Lignes);
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

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = null;

            var type = SelectedType == TypeTiersOption.LesDeux
                ? Domain.Enums.TypeTiers.LesDeux
                : Domain.Enums.TypeTiers.Fournisseur;

            if (_editingId is null)
            {
                await _fournisseurs.CreateAsync(new CreateTiersDto(
                    type,
                    Nom.Trim(),
                    NullIfEmpty(Ice),
                    NullIfEmpty(Adresse),
                    NullIfEmpty(Ville),
                    NullIfEmpty(Telephone),
                    NullIfEmpty(Email),
                    NullIfEmpty(ConditionsPaiement),
                    Actif));
            }
            else
            {
                await _fournisseurs.UpdateAsync(_editingId.Value, new UpdateTiersDto(
                    type,
                    Nom.Trim(),
                    NullIfEmpty(Ice),
                    NullIfEmpty(Adresse),
                    NullIfEmpty(Ville),
                    NullIfEmpty(Telephone),
                    NullIfEmpty(Email),
                    NullIfEmpty(ConditionsPaiement),
                    Actif));
            }

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
    private async Task ExportPdf()
    {
        await _dialogs.ShowErrorAsync("Export PDF bientôt disponible.");
    }

    private static string? NullIfEmpty(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
