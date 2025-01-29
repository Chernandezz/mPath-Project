using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mPathProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctorId = table.Column<long>(type: "bigint", nullable: false),
                    patientId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Discharges",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    treatment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    doctorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    patientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dischargeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    doctorId = table.Column<long>(type: "bigint", nullable: false),
                    admissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discharges", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    observations = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    userRole = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Admissions",
                columns: new[] { "id", "admissionDate", "diagnosis", "doctorId", "observation", "patientId", "patientName" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flu", 1L, "Mild symptoms", 1L, "Alice Johnson" },
                    { 2L, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Broken arm", 2L, "Needs surgery", 2L, "Bob Brown" }
                });

            migrationBuilder.InsertData(
                table: "Discharges",
                columns: new[] { "id", "admissionId", "amount", "dischargeDate", "doctorId", "doctorName", "patientName", "treatment" },
                values: new object[,]
                {
                    { 1L, 1L, 100.00m, new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "John Doe", "Alice Johnson", "Antibiotics" },
                    { 2L, 2L, 500.00m, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "Jane Smith", "Bob Brown", "Surgery" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "id", "active", "email", "firstName", "lastName" },
                values: new object[,]
                {
                    { 1L, true, "john.doe@example.com", "John", "Doe" },
                    { 2L, true, "jane.smith@example.com", "Jane", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "id", "address", "email", "firstName", "lastName", "observations", "phoneNumber" },
                values: new object[,]
                {
                    { 1L, "123 Main St", "alice.johnson@example.com", "Alice", "Johnson", "", "1234567890" },
                    { 2L, "456 Elm St", "bob.brown@example.com", "Bob", "Brown", "", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "password", "userRole" },
                values: new object[,]
                {
                    { 1L, "doctor@doctor.com", "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", "Doctor" },
                    { 2L, "admin@admin.com", "AQAAAAIAAYagAAAAEHAgp0HFG9qc9eOr9W15Dp/qcrk3rbaCDo97hBui3SIlwim0ojzgn8lUQxBwEyWIaQ==", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "Discharges");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
