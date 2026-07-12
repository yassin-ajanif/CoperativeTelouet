using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoperativeTelouet.UI.Services;

public sealed class DocumentColumnVisibilityService : IDocumentColumnVisibilityService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
    };

    private readonly string _filePath;
    private readonly SemaphoreSlim _gate = new(1, 1);

    public DocumentColumnVisibilityService()
        : this(Path.Combine(AppContext.BaseDirectory, "column-visibility.json"))
    {
    }

    public DocumentColumnVisibilityService(string filePath) => _filePath = filePath;

    public async Task<DocumentColumnVisibility> GetAsync(
        string documentKey,
        CancellationToken cancellationToken = default)
    {
        var store = await LoadStoreAsync(cancellationToken).ConfigureAwait(false);
        return Clone(store.Get(documentKey));
    }

    public async Task SaveAsync(
        string documentKey,
        DocumentColumnVisibility visibility,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(visibility);

        await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var store = await LoadStoreUnlockedAsync(cancellationToken).ConfigureAwait(false);
            store.Set(documentKey, Clone(visibility));
            await WriteStoreUnlockedAsync(store, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task<DocumentColumnVisibilityStore> LoadStoreAsync(CancellationToken cancellationToken)
    {
        await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            return await LoadStoreUnlockedAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task<DocumentColumnVisibilityStore> LoadStoreUnlockedAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_filePath))
        {
            var fresh = new DocumentColumnVisibilityStore();
            await WriteStoreUnlockedAsync(fresh, cancellationToken).ConfigureAwait(false);
            return fresh;
        }

        await using var stream = File.OpenRead(_filePath);
        var store = await JsonSerializer
            .DeserializeAsync<DocumentColumnVisibilityStore>(stream, JsonOptions, cancellationToken)
            .ConfigureAwait(false);

        return Normalize(store ?? new DocumentColumnVisibilityStore());
    }

    private async Task WriteStoreUnlockedAsync(
        DocumentColumnVisibilityStore store,
        CancellationToken cancellationToken)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        await using var stream = File.Create(_filePath);
        await JsonSerializer
            .SerializeAsync(stream, Normalize(store), JsonOptions, cancellationToken)
            .ConfigureAwait(false);
    }

    private static DocumentColumnVisibilityStore Normalize(DocumentColumnVisibilityStore store)
    {
        store.DevisClient ??= new DocumentColumnVisibility();
        store.BonCommandeClient ??= new DocumentColumnVisibility();
        store.BonLivraisonClient ??= new DocumentColumnVisibility();
        store.FactureClient ??= new DocumentColumnVisibility();
        store.AvoirClient ??= new DocumentColumnVisibility();
        store.DevisFournisseur ??= new DocumentColumnVisibility();
        store.BonCommandeFournisseur ??= new DocumentColumnVisibility();
        store.BonReceptionFournisseur ??= new DocumentColumnVisibility();
        store.FactureFournisseur ??= new DocumentColumnVisibility();
        store.AvoirFournisseur ??= new DocumentColumnVisibility();
        return store;
    }

    private static DocumentColumnVisibility Clone(DocumentColumnVisibility source) => new()
    {
        ShowColRef = source.ShowColRef,
        ShowColDesignation = source.ShowColDesignation,
        ShowColQte = source.ShowColQte,
        ShowColPrix = source.ShowColPrix,
        ShowColRemise = source.ShowColRemise,
        ShowColTva = source.ShowColTva,
        ShowColConditionnement = source.ShowColConditionnement,
        ShowColHt = source.ShowColHt,
        ShowColTtc = source.ShowColTtc,
    };
}
