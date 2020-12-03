using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class AddOneMoreUnitFor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnPressureUnit",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "VacuumDegreeUnit",
                table: "UsageRecord");

            migrationBuilder.AddColumn<string>(
                name: "HighVacuumDegreeUnit",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LowVacuumDegreeUnit",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemOneColumnPressureUnit",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemTwoColumnPressureUnit",
                table: "UsageRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighVacuumDegreeUnit",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "LowVacuumDegreeUnit",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemOneColumnPressureUnit",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemTwoColumnPressureUnit",
                table: "UsageRecord");

            migrationBuilder.AddColumn<string>(
                name: "ColumnPressureUnit",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VacuumDegreeUnit",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
