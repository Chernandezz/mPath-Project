using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mPathProject.Migrations
{
    /// <inheritdoc />
    public partial class Changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "id",
                keyValue: 2L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "id", "active", "email", "firstName", "lastName" },
                values: new object[,]
                {
                    { 1L, true, "doctor1@gmail.com", "John", "Doe" },
                    { 2L, true, "doctor2@gmail.com", "Emily", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "id", "address", "email", "firstName", "lastName", "observations", "phoneNumber" },
                values: new object[,]
                {
                    { 1L, "123 Oak St", "alice@gmail.com", "Alice", "Johnson", "Asthma patient, requires regular checkups.", "5551234567" },
                    { 2L, "456 Pine Ave", "michael@gmail.com", "Michael", "Brown", "Diabetic, on insulin therapy.", "5559876543" }
                });
        }
    }
}
