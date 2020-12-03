using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class FixNameError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemTwoColumnoPressure",
                table: "UsageRecord");

            migrationBuilder.AddColumn<float>(
                name: "SystemTwoColumnPressure",
                table: "UsageRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemTwoColumnPressure",
                table: "UsageRecord");

            migrationBuilder.AddColumn<float>(
                name: "SystemTwoColumnoPressure",
                table: "UsageRecord",
                type: "real",
                nullable: true);
        }
    }
}
