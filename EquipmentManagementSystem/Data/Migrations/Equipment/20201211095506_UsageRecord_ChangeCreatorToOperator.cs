using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class UsageRecord_ChangeCreatorToOperator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "UsageRecord");

            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "UsageRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operator",
                table: "UsageRecord");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
