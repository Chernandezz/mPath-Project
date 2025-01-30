using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mPathProject.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedStructureAndSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discharges",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Discharges",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Discharges",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Admissions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Admissions",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Admissions",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
