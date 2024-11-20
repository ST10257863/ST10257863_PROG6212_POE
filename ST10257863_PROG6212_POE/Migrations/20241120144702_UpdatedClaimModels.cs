using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10257863_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedClaimModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimApprovals");

            migrationBuilder.DropTable(
                name: "ClaimVerifications");

            migrationBuilder.AddColumn<string>(
                name: "ApprovalComments",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoordinatorId",
                table: "Claims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Claims",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Claims",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Claims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationComments",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationDate",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CoordinatorId",
                table: "Claims",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ManagerId",
                table: "Claims",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_AcademicManagers_ManagerId",
                table: "Claims",
                column: "ManagerId",
                principalTable: "AcademicManagers",
                principalColumn: "ManagerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Coordinators_CoordinatorId",
                table: "Claims",
                column: "CoordinatorId",
                principalTable: "Coordinators",
                principalColumn: "CoordinatorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_AcademicManagers_ManagerId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Coordinators_CoordinatorId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_CoordinatorId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_ManagerId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ApprovalComments",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "CoordinatorId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "VerificationComments",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "VerificationDate",
                table: "Claims");

            migrationBuilder.CreateTable(
                name: "ClaimApprovals",
                columns: table => new
                {
                    ApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimID = table.Column<int>(type: "int", nullable: false),
                    ManagerID = table.Column<int>(type: "int", nullable: false),
                    ApprovalComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimApprovals", x => x.ApprovalID);
                    table.ForeignKey(
                        name: "FK_ClaimApprovals_AcademicManagers_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "AcademicManagers",
                        principalColumn: "ManagerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimApprovals_Claims_ClaimID",
                        column: x => x.ClaimID,
                        principalTable: "Claims",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimVerifications",
                columns: table => new
                {
                    VerificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimID = table.Column<int>(type: "int", nullable: false),
                    CoordinatorID = table.Column<int>(type: "int", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerificationComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimVerifications", x => x.VerificationID);
                    table.ForeignKey(
                        name: "FK_ClaimVerifications_Claims_ClaimID",
                        column: x => x.ClaimID,
                        principalTable: "Claims",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimVerifications_Coordinators_CoordinatorID",
                        column: x => x.CoordinatorID,
                        principalTable: "Coordinators",
                        principalColumn: "CoordinatorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimApprovals_ClaimID",
                table: "ClaimApprovals",
                column: "ClaimID");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimApprovals_ManagerID",
                table: "ClaimApprovals",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimVerifications_ClaimID",
                table: "ClaimVerifications",
                column: "ClaimID");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimVerifications_CoordinatorID",
                table: "ClaimVerifications",
                column: "CoordinatorID");
        }
    }
}
