namespace CoperativeTelouet.Business.DTOs;

public record ChambreFroideDto(int Id, string Nom, int CapaciteBacs, bool Actif);

public record CreateChambreFroideDto(string Nom, int CapaciteBacs, bool Actif = true);

public record UpdateChambreFroideDto(string Nom, int CapaciteBacs, bool Actif);
