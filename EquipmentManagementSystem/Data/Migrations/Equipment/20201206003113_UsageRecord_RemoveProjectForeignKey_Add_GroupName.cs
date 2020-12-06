using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class UsageRecord_RemoveProjectForeignKey_Add_GroupName : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "Detector",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IonSource",
                table: "UsageRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detector",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "IonSource",
                table: "UsageRecord");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "UsageRecord",
                type: "int",
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
    }
}
