using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10257863_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class AddingHourlyRateToClaimTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Claims");
        }
    }
}
