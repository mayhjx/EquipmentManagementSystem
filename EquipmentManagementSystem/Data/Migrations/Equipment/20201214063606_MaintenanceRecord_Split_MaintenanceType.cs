using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class MaintenanceRecord_Split_MaintenanceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "OtherContent",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MaintenanceRecords");

            migrationBuilder.AddColumn<string>(
                name: "Daily",
                table: "MaintenanceRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Monthly",
                table: "MaintenanceRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quarterly",
                table: "MaintenanceRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Temporary",
                table: "MaintenanceRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weekly",
                table: "MaintenanceRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Yearly",
                table: "MaintenanceRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Daily",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "Monthly",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "Quarterly",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "Temporary",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "Weekly",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "Yearly",
                table: "MaintenanceRecords");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "MaintenanceRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherContent",
                table: "MaintenanceRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MaintenanceRecords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
