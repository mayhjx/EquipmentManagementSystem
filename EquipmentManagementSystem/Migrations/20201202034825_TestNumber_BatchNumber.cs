using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class TestNumber_BatchNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemOneTestNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemTwoTestNumber",
                table: "UsageRecord");

            migrationBuilder.AddColumn<int>(
                name: "SystemOneBatchNumber",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemTwoBatchNumber",
                table: "UsageRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemOneBatchNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemTwoBatchNumber",
                table: "UsageRecord");

            migrationBuilder.AddColumn<int>(
                name: "SystemOneTestNumber",
                table: "UsageRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemTwoTestNumber",
                table: "UsageRecord",
                type: "int",
                nullable: true);
        }
    }
}
