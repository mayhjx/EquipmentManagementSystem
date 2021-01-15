using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class ChangeFKName_UsageRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsageRecord_Instrument_InstrumentId",
                table: "UsageRecord");

            migrationBuilder.RenameColumn(
                name: "InstrumentId",
                table: "UsageRecord",
                newName: "InstrumentID");

            migrationBuilder.RenameIndex(
                name: "IX_UsageRecord_InstrumentId",
                table: "UsageRecord",
                newName: "IX_UsageRecord_InstrumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRecord_Instrument_InstrumentID",
                table: "UsageRecord",
                column: "InstrumentID",
                principalTable: "Instrument",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsageRecord_Instrument_InstrumentID",
                table: "UsageRecord");

            migrationBuilder.RenameColumn(
                name: "InstrumentID",
                table: "UsageRecord",
                newName: "InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_UsageRecord_InstrumentID",
                table: "UsageRecord",
                newName: "IX_UsageRecord_InstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRecord_Instrument_InstrumentId",
                table: "UsageRecord",
                column: "InstrumentId",
                principalTable: "Instrument",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
