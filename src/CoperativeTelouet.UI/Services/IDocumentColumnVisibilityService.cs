namespace CoperativeTelouet.UI.Services;

public interface IDocumentColumnVisibilityService
{
    Task<DocumentColumnVisibility> GetAsync(string documentKey, CancellationToken cancellationToken = default);
    Task SaveAsync(string documentKey, DocumentColumnVisibility visibility, CancellationToken cancellationToken = default);
}
