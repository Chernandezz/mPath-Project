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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admissions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admissions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discharges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Treatment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DischargeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    AdmissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discharges_Admissions_AdmissionId",
                        column: x => x.AdmissionId,
                        principalTable: "Admissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "UserRole" },
                values: new object[,]
                {
                    { 1L, "doctor1@hospital.com", "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", "Doctor" },
                    { 2L, "doctor2@hospital.com", "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", "Doctor" },
                    { 3L, "admin@hospital.com", "AQAAAAIAAYagAAAAEHAgp0HFG9qc9eOr9W15Dp/qcrk3rbaCDo97hBui3SIlwim0ojzgn8lUQxBwEyWIaQ==", "Admin" },
                    { 4L, "patient1@hospital.com", "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", "Patient" },
                    { 5L, "patient2@hospital.com", "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", "Patient" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Active", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1L, true, "John", "Doe" },
                    { 2L, true, "Jane", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "Observations", "PhoneNumber" },
                values: new object[,]
                {
                    { 1L, "123 Main St", "Alice", "Johnson", "Diabetic", "1234567890" },
                    { 2L, "456 Elm St", "Bob", "Brown", "Asthma", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Admissions",
                columns: new[] { "Id", "AdmissionDate", "Diagnosis", "DoctorId", "Observation", "PatientId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flu", 1L, "Mild symptoms", 1L },
                    { 2L, new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Broken Arm", 2L, "Needs surgery", 2L },
                    { 3L, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Migraine", 1L, "Severe headaches", 1L }
                });

            migrationBuilder.InsertData(
                table: "Discharges",
                columns: new[] { "Id", "AdmissionId", "Amount", "DischargeDate", "IsPaid", "Treatment" },
                values: new object[,]
                {
                    { 1L, 1L, 100.00m, new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Antibiotics" },
                    { 2L, 2L, 500.00m, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Surgery" },
                    { 3L, 3L, 75.00m, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Painkillers" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_DoctorId",
                table: "Admissions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_PatientId",
                table: "Admissions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_AdmissionId",
                table: "Discharges",
                column: "AdmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discharges");

            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
