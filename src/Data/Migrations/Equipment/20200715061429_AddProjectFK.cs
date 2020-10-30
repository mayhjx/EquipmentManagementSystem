using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class AddProjectFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "UsageRecord",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
