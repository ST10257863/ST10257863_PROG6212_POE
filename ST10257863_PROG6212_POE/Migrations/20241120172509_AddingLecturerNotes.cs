using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10257863_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class AddingLecturerNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LecturerNotes",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LecturerNotes",
                table: "Claims");
        }
    }
}
