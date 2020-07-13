using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class RemoveProjectFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsageRecord_Project_ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "UsageRecord");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "UsageRecord",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Solution",
                table: "Maintenance",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(999)",
                oldMaxLength: 999,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "UsageRecord",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "UsageRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Solution",
                table: "Maintenance",
                type: "nvarchar(999)",
                maxLength: 999,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRecord_Project_ProjectId",
                table: "UsageRecord",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
