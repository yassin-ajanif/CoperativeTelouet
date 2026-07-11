namespace CoperativeTelouet.Business.DTOs;

public record CategorieDto(int Id, string Nom);

public record CreateCategorieDto(string Nom);

public record UpdateCategorieDto(string Nom);
