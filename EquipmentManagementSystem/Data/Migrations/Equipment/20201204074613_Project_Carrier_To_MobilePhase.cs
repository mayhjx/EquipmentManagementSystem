using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class Project_Carrier_To_MobilePhase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "MobilePhase",
                table: "Project",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobilePhase",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "Carrier",
                table: "Project",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
