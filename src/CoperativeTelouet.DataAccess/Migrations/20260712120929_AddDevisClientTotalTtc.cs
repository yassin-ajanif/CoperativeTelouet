using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoperativeTelouet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDevisClientTotalTtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalTtc",
                table: "DevisClient",
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
                table: "DevisClient");
        }
    }
}
