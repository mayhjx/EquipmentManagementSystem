using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class RemoveMaintenanceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceContent_MaintenanceType_MaintenanceTypeId",
                table: "MaintenanceContent");

            migrationBuilder.DropTable(
                name: "MaintenanceType");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceContent_MaintenanceTypeId",
                table: "MaintenanceContent");

            migrationBuilder.DropColumn(
                name: "MaintenanceTypeId",
                table: "MaintenanceContent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTypeId",
                table: "MaintenanceContent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaintenanceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentPlatform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceContent_MaintenanceTypeId",
                table: "MaintenanceContent",
                column: "MaintenanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceContent_MaintenanceType_MaintenanceTypeId",
                table: "MaintenanceContent",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
