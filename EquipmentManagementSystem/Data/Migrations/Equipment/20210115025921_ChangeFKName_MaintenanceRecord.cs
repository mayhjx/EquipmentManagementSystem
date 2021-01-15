using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class ChangeFKName_MaintenanceRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_Instrument_InstrumentId",
                table: "MaintenanceRecords");

            migrationBuilder.RenameColumn(
                name: "InstrumentId",
                table: "MaintenanceRecords",
                newName: "InstrumentID");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRecords_InstrumentId",
                table: "MaintenanceRecords",
                newName: "IX_MaintenanceRecords_InstrumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_Instrument_InstrumentID",
                table: "MaintenanceRecords",
                column: "InstrumentID",
                principalTable: "Instrument",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRecords_Instrument_InstrumentID",
                table: "MaintenanceRecords");

            migrationBuilder.RenameColumn(
                name: "InstrumentID",
                table: "MaintenanceRecords",
                newName: "InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRecords_InstrumentID",
                table: "MaintenanceRecords",
                newName: "IX_MaintenanceRecords_InstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRecords_Instrument_InstrumentId",
                table: "MaintenanceRecords",
                column: "InstrumentId",
                principalTable: "Instrument",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
