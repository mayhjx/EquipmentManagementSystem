using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class CombineMaintenanceRecordModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HalfYearly",
                table: "MaintenanceRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HalfYearly",
                table: "MaintenanceRecords");
        }
    }
}
