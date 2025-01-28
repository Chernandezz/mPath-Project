using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mPathProject.Migrations
{
    /// <inheritdoc />
    public partial class InititalMigration : Migration
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
