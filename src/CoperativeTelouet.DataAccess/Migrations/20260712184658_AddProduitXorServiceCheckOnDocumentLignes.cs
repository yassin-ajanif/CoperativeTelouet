using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoperativeTelouet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProduitXorServiceCheckOnDocumentLignes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_FactureFournisseurLignes_ProduitXorService",
                table: "FactureFournisseurLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_FactureClientLignes_ProduitXorService",
                table: "FactureClientLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_DevisFournisseurLignes_ProduitXorService",
                table: "DevisFournisseurLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_DevisClientLignes_ProduitXorService",
                table: "DevisClientLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BonReceptionFournisseurLignes_ProduitXorService",
                table: "BonReceptionFournisseurLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BonLivraisonClientLignes_ProduitXorService",
                table: "BonLivraisonClientLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BonCommandeFournisseurLignes_ProduitXorService",
                table: "BonCommandeFournisseurLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BonCommandeClientLignes_ProduitXorService",
                table: "BonCommandeClientLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AvoirFournisseurLignes_ProduitXorService",
                table: "AvoirFournisseurLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AvoirClientLignes_ProduitXorService",
                table: "AvoirClientLignes",
                sql: "(ProduitId IS NOT NULL AND ServiceId IS NULL) OR (ProduitId IS NULL AND ServiceId IS NOT NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_FactureFournisseurLignes_ProduitXorService",
                table: "FactureFournisseurLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_FactureClientLignes_ProduitXorService",
                table: "FactureClientLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_DevisFournisseurLignes_ProduitXorService",
                table: "DevisFournisseurLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_DevisClientLignes_ProduitXorService",
                table: "DevisClientLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BonReceptionFournisseurLignes_ProduitXorService",
                table: "BonReceptionFournisseurLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BonLivraisonClientLignes_ProduitXorService",
                table: "BonLivraisonClientLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BonCommandeFournisseurLignes_ProduitXorService",
                table: "BonCommandeFournisseurLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BonCommandeClientLignes_ProduitXorService",
                table: "BonCommandeClientLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AvoirFournisseurLignes_ProduitXorService",
                table: "AvoirFournisseurLignes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AvoirClientLignes_ProduitXorService",
                table: "AvoirClientLignes");
        }
    }
}
