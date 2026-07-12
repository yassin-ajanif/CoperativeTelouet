using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoperativeTelouet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalTtcToClientDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalTtc",
                table: "DevisFournisseur",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTtc",
                table: "BonsLivraisonClient",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTtc",
                table: "BonsCommandeFournisseur",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTtc",
                table: "BonsCommandeClient",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTtc",
                table: "AvoirsFournisseur",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTtc",
                table: "AvoirsClient",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalTtc",
                table: "DevisFournisseur");

            migrationBuilder.DropColumn(
                name: "TotalTtc",
                table: "BonsLivraisonClient");

            migrationBuilder.DropColumn(
                name: "TotalTtc",
                table: "BonsCommandeFournisseur");

            migrationBuilder.DropColumn(
                name: "TotalTtc",
                table: "BonsCommandeClient");

            migrationBuilder.DropColumn(
                name: "TotalTtc",
                table: "AvoirsFournisseur");

            migrationBuilder.DropColumn(
                name: "TotalTtc",
                table: "AvoirsClient");
        }
    }
}
