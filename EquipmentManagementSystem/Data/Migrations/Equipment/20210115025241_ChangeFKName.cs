using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class ChangeFKName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assert_Instrument_InstrumentId",
                table: "Assert");

            migrationBuilder.DropIndex(
                name: "IX_Assert_InstrumentId",
                table: "Assert");

            migrationBuilder.RenameColumn(
                name: "InstrumentId",
                table: "Assert",
                newName: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Assert_InstrumentID",
                table: "Assert",
                column: "InstrumentID",
                unique: true,
                filter: "[InstrumentID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Assert_Instrument_InstrumentID",
                table: "Assert",
                column: "InstrumentID",
                principalTable: "Instrument",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assert_Instrument_InstrumentID",
                table: "Assert");

            migrationBuilder.DropIndex(
                name: "IX_Assert_InstrumentID",
                table: "Assert");

            migrationBuilder.RenameColumn(
                name: "InstrumentID",
                table: "Assert",
                newName: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assert_InstrumentId",
                table: "Assert",
                column: "InstrumentId",
                unique: true,
                filter: "[InstrumentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Assert_Instrument_InstrumentId",
                table: "Assert",
                column: "InstrumentId",
                principalTable: "Instrument",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
