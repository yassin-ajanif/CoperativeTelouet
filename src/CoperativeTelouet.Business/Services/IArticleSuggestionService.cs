using CoperativeTelouet.Business.DTOs.Client;

namespace CoperativeTelouet.Business.Services;

public interface IArticleSuggestionService
{
    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);
}
