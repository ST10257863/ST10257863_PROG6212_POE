using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10257863_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class AddFilePathToClaimFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ClaimFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ClaimFiles");
        }
    }
}
