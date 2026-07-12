namespace CoperativeTelouet.Business.DTOs;

public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount);
