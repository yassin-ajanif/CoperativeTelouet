namespace CoperativeTelouet.Business.DTOs;

public record TypeChargeDto(int Id, string Nom, bool Actif);

public record CreateTypeChargeDto(string Nom, bool Actif = true);

public record UpdateTypeChargeDto(string Nom, bool Actif);
