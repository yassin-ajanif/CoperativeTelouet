using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services.Client;
using CoperativeTelouet.UI.Services;
using FluentValidation;

namespace CoperativeTelouet.UI.ViewModels.Vente.Clients;

public partial class ClientDetailViewModel : ViewModelBase
{
    private readonly IClientService _clients;
    private readonly IUserDialogService _dialogs;
    private ClientsViewModel? _host;
    private int? _editingId;

    [ObservableProperty]
    private TypeTiersOption _selectedType = TypeTiersOption.Client;

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
    private ObservableCollection<ClientCompteLigneDto> _compteLignes = [];

    [ObservableProperty]
    private bool _showCompte;

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    public string SoldeLabel => $"Solde actuel : {SoldeActuel:N2} DH";
    public IReadOnlyList<TypeTiersOption> TypeOptions { get; } =
    [
        TypeTiersOption.Client,
        TypeTiersOption.LesDeux,
    ];

    public bool IsNew => _editingId is null;

    public ClientDetailViewModel(IClientService clients, IUserDialogService dialogs)
    {
        _clients = clients;
        _dialogs = dialogs;
    }

    public void AttachHost(ClientsViewModel host) => _host = host;

    partial void OnErrorMessageChanged(string? value) => OnPropertyChanged(nameof(HasError));
    partial void OnSoldeActuelChanged(decimal value) => OnPropertyChanged(nameof(SoldeLabel));

    public async Task LoadAsync(int? clientId)
    {
        _editingId = clientId;
        OnPropertyChanged(nameof(IsNew));
        ErrorMessage = null;
        CompteLignes = [];
        ShowCompte = clientId is not null;

        if (clientId is null)
        {
            SelectedType = TypeTiersOption.Client;
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
            var dto = await _clients.GetClientByIdAsync(clientId.Value)
                ?? throw new KeyNotFoundException("Client introuvable.");

            SelectedType = dto.Type == Domain.Enums.TypeTiers.LesDeux
                ? TypeTiersOption.LesDeux
                : TypeTiersOption.Client;
            Nom = dto.Nom;
            Ice = dto.ICE;
            Adresse = dto.Adresse;
            Ville = dto.Ville;
            Telephone = dto.Telephone;
            Email = dto.Email;
            ConditionsPaiement = dto.ConditionsPaiement;
            Actif = dto.Actif;

            var compte = await _clients.GetCompteAsync(clientId.Value);
            SoldeActuel = compte.SoldeActuel;
            CompteLignes = new ObservableCollection<ClientCompteLigneDto>(compte.Lignes);
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
                : Domain.Enums.TypeTiers.Client;

            if (_editingId is null)
            {
                var created = await _clients.CreateClientAsync(new CreateTiersDto(
                    type,
                    Nom.Trim(),
                    NullIfEmpty(Ice),
                    NullIfEmpty(Adresse),
                    NullIfEmpty(Ville),
                    NullIfEmpty(Telephone),
                    NullIfEmpty(Email),
                    NullIfEmpty(ConditionsPaiement),
                    Actif));
                _editingId = created.Id;
                OnPropertyChanged(nameof(IsNew));
                ShowCompte = true;
            }
            else
            {
                await _clients.UpdateClientAsync(_editingId.Value, new UpdateTiersDto(
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

            if (_editingId is int id)
            {
                var compte = await _clients.GetCompteAsync(id);
                SoldeActuel = compte.SoldeActuel;
                CompteLignes = new ObservableCollection<ClientCompteLigneDto>(compte.Lignes);
            }

            await _dialogs.ShowSuccessAsync("Enregistrement réussi.");
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
