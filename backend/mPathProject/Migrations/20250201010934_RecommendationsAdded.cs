using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mPathProject.Migrations
{
    /// <inheritdoc />
    public partial class RecommendationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Treatment",
                table: "Discharges");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Discharges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Recommendation",
                table: "Discharges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "Recommendation",
                table: "Discharges");

            migrationBuilder.AddColumn<string>(
                name: "Treatment",
                table: "Discharges",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
