using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10257863_PROG6212_POE.Migrations
{
	/// <inheritdoc />
	public partial class InitialCreate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					UserID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.UserID);
				});

			migrationBuilder.CreateTable(
				name: "AcademicManagers",
				columns: table => new
				{
					ManagerID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserID = table.Column<int>(type: "int", nullable: false),
					Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Campus = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AcademicManagers", x => x.ManagerID);
					table.ForeignKey(
						name: "FK_AcademicManagers_Users_UserID",
						column: x => x.UserID,
						principalTable: "Users",
						principalColumn: "UserID",
						onDelete: ReferentialAction.Cascade); // Keep cascade delete for Users
				});

			migrationBuilder.CreateTable(
				name: "Coordinators",
				columns: table => new
				{
					CoordinatorID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserID = table.Column<int>(type: "int", nullable: false),
					Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Campus = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Coordinators", x => x.CoordinatorID);
					table.ForeignKey(
						name: "FK_Coordinators_Users_UserID",
						column: x => x.UserID,
						principalTable: "Users",
						principalColumn: "UserID",
						onDelete: ReferentialAction.Cascade); // Keep cascade delete for Users
				});

			migrationBuilder.CreateTable(
				name: "Lecturers",
				columns: table => new
				{
					LecturerID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserID = table.Column<int>(type: "int", nullable: false),
					HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Campus = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Lecturers", x => x.LecturerID);
					table.ForeignKey(
						name: "FK_Lecturers_Users_UserID",
						column: x => x.UserID,
						principalTable: "Users",
						principalColumn: "UserID",
						onDelete: ReferentialAction.Cascade); // Keep cascade delete for Users
				});

			migrationBuilder.CreateTable(
				name: "Claims",
				columns: table => new
				{
					ClaimId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					LecturerId = table.Column<int>(type: "int", nullable: false),
					HoursWorked = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
					SupportingDocuments = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Claims", x => x.ClaimId);
					table.ForeignKey(
						name: "FK_Claims_Lecturers_LecturerId",
						column: x => x.LecturerId,
						principalTable: "Lecturers",
						principalColumn: "LecturerID",
						onDelete: ReferentialAction.Restrict); // No cascade delete
				});

			migrationBuilder.CreateTable(
				name: "ClaimApprovals",
				columns: table => new
				{
					ApprovalID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ClaimID = table.Column<int>(type: "int", nullable: false),
					ManagerID = table.Column<int>(type: "int", nullable: false),
					ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					IsApproved = table.Column<bool>(type: "bit", nullable: false),
					ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ApprovalComments = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ClaimApprovals", x => x.ApprovalID);
					table.ForeignKey(
						name: "FK_ClaimApprovals_AcademicManagers_ManagerID",
						column: x => x.ManagerID,
						principalTable: "AcademicManagers",
						principalColumn: "ManagerID",
						onDelete: ReferentialAction.Restrict); // No cascade delete

					table.ForeignKey(
						name: "FK_ClaimApprovals_Claims_ClaimID",
						column: x => x.ClaimID,
						principalTable: "Claims",
						principalColumn: "ClaimId",
						onDelete: ReferentialAction.Restrict); // No cascade delete
				});

			migrationBuilder.CreateTable(
				name: "ClaimVerifications",
				columns: table => new
				{
					VerificationID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ClaimID = table.Column<int>(type: "int", nullable: false),
					CoordinatorID = table.Column<int>(type: "int", nullable: false),
					VerificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
					IsVerified = table.Column<bool>(type: "bit", nullable: false),
					VerificationComments = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ClaimVerifications", x => x.VerificationID);
					table.ForeignKey(
						name: "FK_ClaimVerifications_Claims_ClaimID",
						column: x => x.ClaimID,
						principalTable: "Claims",
						principalColumn: "ClaimId",
						onDelete: ReferentialAction.Restrict); // No cascade delete

					table.ForeignKey(
						name: "FK_ClaimVerifications_Coordinators_CoordinatorID",
						column: x => x.CoordinatorID,
						principalTable: "Coordinators",
						principalColumn: "CoordinatorID",
						onDelete: ReferentialAction.Restrict); // No cascade delete
				});

			migrationBuilder.CreateIndex(
				name: "IX_AcademicManagers_UserID",
				table: "AcademicManagers",
				column: "UserID");

			migrationBuilder.CreateIndex(
				name: "IX_ClaimApprovals_ClaimID",
				table: "ClaimApprovals",
				column: "ClaimID");

			migrationBuilder.CreateIndex(
				name: "IX_ClaimApprovals_ManagerID",
				table: "ClaimApprovals",
				column: "ManagerID");

			migrationBuilder.CreateIndex(
				name: "IX_Claims_LecturerId",
				table: "Claims",
				column: "LecturerId");

			migrationBuilder.CreateIndex(
				name: "IX_ClaimVerifications_ClaimID",
				table: "ClaimVerifications",
				column: "ClaimID");

			migrationBuilder.CreateIndex(
				name: "IX_ClaimVerifications_CoordinatorID",
				table: "ClaimVerifications",
				column: "CoordinatorID");

			migrationBuilder.CreateIndex(
				name: "IX_Coordinators_UserID",
				table: "Coordinators",
				column: "UserID");

			migrationBuilder.CreateIndex(
				name: "IX_Lecturers_UserID",
				table: "Lecturers",
				column: "UserID");
		}


		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ClaimApprovals");

			migrationBuilder.DropTable(
				name: "ClaimVerifications");

			migrationBuilder.DropTable(
				name: "AcademicManagers");

			migrationBuilder.DropTable(
				name: "Claims");

			migrationBuilder.DropTable(
				name: "Coordinators");

			migrationBuilder.DropTable(
				name: "Lecturers");

			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
