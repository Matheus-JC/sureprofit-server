using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SureProfit.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGrossProfitToStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GrossProfit",
                table: "Stores",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossProfit",
                table: "Stores");
        }
    }
}
