using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class ChangeMaintenanceContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceContent_MaintenanceType_MaintenanceTypeId",
                table: "MaintenanceContent");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceTypeId",
                table: "MaintenanceContent",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "InstrumentPlatform",
                table: "MaintenanceContent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MaintenanceContent",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceContent_MaintenanceType_MaintenanceTypeId",
                table: "MaintenanceContent",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceContent_MaintenanceType_MaintenanceTypeId",
                table: "MaintenanceContent");

            migrationBuilder.DropColumn(
                name: "InstrumentPlatform",
                table: "MaintenanceContent");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MaintenanceContent");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceTypeId",
                table: "MaintenanceContent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceContent_MaintenanceType_MaintenanceTypeId",
                table: "MaintenanceContent",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
