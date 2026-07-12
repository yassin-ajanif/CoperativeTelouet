namespace CoperativeTelouet.Business.Services;

public static class DocumentTotals
{
    public static decimal LineHt(decimal quantite, decimal prixUnitaireHt, decimal remise) =>
        quantite * prixUnitaireHt - remise;

    public static decimal LineTtc(decimal quantite, decimal prixUnitaireHt, decimal remise, decimal tauxTva)
    {
        var ht = LineHt(quantite, prixUnitaireHt, remise);
        return ht * (1 + tauxTva / 100m);
    }

    public static (decimal Ht, decimal Tva, decimal Ttc) Compute(
        IEnumerable<(decimal Quantite, decimal PrixUnitaireHT, decimal Remise, decimal TauxTVA)> lignes,
        decimal remiseGlobale = 0)
    {
        decimal ht = 0, tva = 0;
        foreach (var (quantite, prixUnitaireHt, remise, tauxTva) in lignes)
        {
            var lineHt = LineHt(quantite, prixUnitaireHt, remise);
            ht += lineHt;
            tva += lineHt * (tauxTva / 100m);
        }

        ht = Math.Max(0, ht - remiseGlobale);
        return (ht, tva, ht + tva);
    }
}
