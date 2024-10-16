using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ST10257863_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class DropAndReinitializeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicManagers_Users_UserID",
                table: "AcademicManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinators_Users_UserID",
                table: "Coordinators");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Users_UserID",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_AcademicManagers_UserID",
                table: "AcademicManagers");

            migrationBuilder.DeleteData(
                table: "AcademicManagers",
                keyColumn: "ManagerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AcademicManagers",
                keyColumn: "ManagerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coordinators",
                keyColumn: "CoordinatorID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coordinators",
                keyColumn: "CoordinatorID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "ContactInfo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AcademicManagers",
                columns: new[] { "ManagerID", "Campus", "Department", "UserID" },
                values: new object[,]
                {
                    { 3001, "Main Campus", "IT", 1 },
                    { 3002, "East Campus", "Business", 4 }
                });

            migrationBuilder.InsertData(
                table: "Coordinators",
                columns: new[] { "CoordinatorID", "Campus", "Department", "UserID" },
                values: new object[,]
                {
                    { 2001, "North Campus", "Engineering", 1 },
                    { 2002, "West Campus", "Science", 3 }
                });

            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "LecturerID", "Campus", "Department", "HourlyRate", "UserID" },
                values: new object[,]
                {
                    { 1001, "Main Campus", "Computer Science", 500.00m, 1 },
                    { 1002, "South Campus", "Mathematics", 450.00m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicManagers_UserID",
                table: "AcademicManagers",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicManagers_Users_UserID",
                table: "AcademicManagers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinators_Users_UserID",
                table: "Coordinators",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Users_UserID",
                table: "Lecturers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicManagers_Users_UserID",
                table: "AcademicManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinators_Users_UserID",
                table: "Coordinators");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Users_UserID",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_AcademicManagers_UserID",
                table: "AcademicManagers");

            migrationBuilder.DeleteData(
                table: "AcademicManagers",
                keyColumn: "ManagerID",
                keyValue: 3001);

            migrationBuilder.DeleteData(
                table: "AcademicManagers",
                keyColumn: "ManagerID",
                keyValue: 3002);

            migrationBuilder.DeleteData(
                table: "Coordinators",
                keyColumn: "CoordinatorID",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "Coordinators",
                keyColumn: "CoordinatorID",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 1002);

            migrationBuilder.AlterColumn<string>(
                name: "ContactInfo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AcademicManagers",
                columns: new[] { "ManagerID", "Campus", "Department", "UserID" },
                values: new object[,]
                {
                    { 1, "Main Campus", "IT", 1 },
                    { 2, "East Campus", "Business", 4 }
                });

            migrationBuilder.InsertData(
                table: "Coordinators",
                columns: new[] { "CoordinatorID", "Campus", "Department", "UserID" },
                values: new object[,]
                {
                    { 1, "North Campus", "Engineering", 1 },
                    { 2, "West Campus", "Science", 3 }
                });

            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "LecturerID", "Campus", "Department", "HourlyRate", "UserID" },
                values: new object[,]
                {
                    { 1, "Main Campus", "Computer Science", 500.00m, 1 },
                    { 2, "South Campus", "Math", 450.00m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicManagers_UserID",
                table: "AcademicManagers",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicManagers_Users_UserID",
                table: "AcademicManagers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinators_Users_UserID",
                table: "Coordinators",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Users_UserID",
                table: "Lecturers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
