using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class changetogroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "ProjectTeamName",
                table: "Instrument",
                newName: "Group");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Group",
                table: "Instrument",
                newName: "ProjectTeamName");
        }
    }
}
