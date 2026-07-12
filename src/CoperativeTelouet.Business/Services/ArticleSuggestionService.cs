using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities;

namespace CoperativeTelouet.Business.Services;

public class ArticleSuggestionService : IArticleSuggestionService
{
    private readonly IRepository<Produit> _produits;
    private readonly IRepository<ServiceItem> _services;

    public ArticleSuggestionService(
        IRepository<Produit> produits,
        IRepository<ServiceItem> services)
    {
        _produits = produits;
        _services = services;
    }

    public async Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default)
    {
        var q = search?.Trim() ?? string.Empty;
        var produits = await _produits.FindAsync(p => p.Actif, cancellationToken);
        var services = await _services.FindAsync(s => s.Actif, cancellationToken);

        IEnumerable<ArticleSuggestionDto> fromProduits = produits.Select(p => new ArticleSuggestionDto(
            p.Id, null, p.Reference, p.Designation, p.Unite, p.PrixVenteHT, p.TauxTVA));

        IEnumerable<ArticleSuggestionDto> fromServices = services.Select(s => new ArticleSuggestionDto(
            null, s.Id, s.Reference, s.Designation, s.Unite, s.PrixVenteHT, s.TauxTVA));

        var all = fromProduits.Concat(fromServices);
        if (!string.IsNullOrEmpty(q))
        {
            all = all.Where(a =>
                a.Reference.Contains(q, StringComparison.OrdinalIgnoreCase)
                || a.Designation.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        return all.OrderBy(a => a.Designation).Take(30).ToList();
    }
}
