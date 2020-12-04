using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class addSystemTwoTestSignal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestSignal",
                table: "UsageRecord");

            migrationBuilder.AddColumn<float>(
                name: "SystemOneTestSignal",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SystemTwoTestSignal",
                table: "UsageRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemOneTestSignal",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemTwoTestSignal",
                table: "UsageRecord");

            migrationBuilder.AddColumn<float>(
                name: "TestSignal",
                table: "UsageRecord",
                type: "real",
                nullable: true);
        }
    }
}
