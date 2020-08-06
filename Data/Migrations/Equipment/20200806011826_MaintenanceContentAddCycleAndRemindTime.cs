using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class MaintenanceContentAddCycleAndRemindTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cycle",
                table: "MaintenanceType");

            migrationBuilder.DropColumn(
                name: "RemindTime",
                table: "MaintenanceType");

            migrationBuilder.AddColumn<int>(
                name: "Cycle",
                table: "MaintenanceContent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RemindTime",
                table: "MaintenanceContent",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cycle",
                table: "MaintenanceContent");

            migrationBuilder.DropColumn(
                name: "RemindTime",
                table: "MaintenanceContent");

            migrationBuilder.AddColumn<int>(
                name: "Cycle",
                table: "MaintenanceType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RemindTime",
                table: "MaintenanceType",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
