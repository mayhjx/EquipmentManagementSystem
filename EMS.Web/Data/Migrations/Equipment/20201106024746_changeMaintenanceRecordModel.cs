using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class changeMaintenanceRecordModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_Project_ProjectId",
                table: "MaintenanceRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRecords_ProjectId",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "MaintenanceRecords");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "MaintenanceRecords",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "MaintenanceRecords");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "MaintenanceRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_ProjectId",
                table: "MaintenanceRecords",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_Project_ProjectId",
                table: "MaintenanceRecords",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
