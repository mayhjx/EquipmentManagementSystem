using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class AddCarrierAndDetector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Carrier",
                table: "Project",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detector",
                table: "Project",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Detector",
                table: "Project");
        }
    }
}
