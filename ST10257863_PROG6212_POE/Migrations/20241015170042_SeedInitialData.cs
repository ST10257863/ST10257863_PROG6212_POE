using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ST10257863_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "ContactInfo", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "Admin", "User", "adminPass123", "admin" },
                    { 2, "lecturer@example.com", "John", "Doe", "LecturerPass123", "lecturerUser" },
                    { 3, "coordinator@example.com", "Jane", "Smith", "CoordinatorPass123", "coordinatorUser" },
                    { 4, "manager@example.com", "Mike", "Johnson", "ManagerPass123", "managerUser" }
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);
        }
    }
}
