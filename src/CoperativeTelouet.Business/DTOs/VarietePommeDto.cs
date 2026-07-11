namespace CoperativeTelouet.Business.DTOs;

public record VarietePommeDto(int Id, string Nom);

public record CreateVarietePommeDto(string Nom);

public record UpdateVarietePommeDto(string Nom);
