using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10257863_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClaimFilesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "ClaimFiles");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "ClaimFiles",
                newName: "ClaimFileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimFileId",
                table: "ClaimFiles",
                newName: "FileId");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "ClaimFiles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
