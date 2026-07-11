using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoperativeTelouet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SyncPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "VarietesPomme",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "TypesCharge",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Tiers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "StockBacsSociete",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Services",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Produits",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "PaiementsFournisseur",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "PaiementsClient",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "MouvementsStock",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "FacturesFournisseur",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "FacturesClient",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "FactureFournisseurLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "FactureClientLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "DevisFournisseurLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "DevisFournisseurConditions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "DevisFournisseur",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "DevisClientLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "DevisClientConditions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "DevisClient",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Charges",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "ChambresFroides",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Categories",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonsSortieStockage",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonsReceptionFournisseur",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonsLivraisonClient",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonsEntreeStockage",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonsCommandeFournisseur",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonsCommandeClient",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonReceptionFournisseurLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonLivraisonClientLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonCommandeFournisseurLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "BonCommandeClientLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "AvoirsFournisseur",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "AvoirsClient",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "AvoirFournisseurLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "AvoirClientLignes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "AppSettings",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "VarietesPomme");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "TypesCharge");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Tiers");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "StockBacsSociete");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "PaiementsFournisseur");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "PaiementsClient");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "MouvementsStock");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "FacturesFournisseur");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "FacturesClient");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "FactureFournisseurLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "FactureClientLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "DevisFournisseurLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "DevisFournisseurConditions");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "DevisFournisseur");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "DevisClientLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "DevisClientConditions");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "DevisClient");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "ChambresFroides");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonsSortieStockage");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonsReceptionFournisseur");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonsLivraisonClient");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonsEntreeStockage");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonsCommandeFournisseur");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonsCommandeClient");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonReceptionFournisseurLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonLivraisonClientLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonCommandeFournisseurLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BonCommandeClientLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "AvoirsFournisseur");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "AvoirsClient");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "AvoirFournisseurLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "AvoirClientLignes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "AppSettings");
        }
    }
}
