using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class MaintenanceRecord_Add_GroupName_Remove_ProjectFK : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MaintenanceRecords",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "MaintenanceRecords",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Operator",
                table: "MaintenanceRecords",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "MaintenanceRecords",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "MaintenanceRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "MaintenanceRecords");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MaintenanceRecords",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "MaintenanceRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Operator",
                table: "MaintenanceRecords",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "MaintenanceRecords",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
