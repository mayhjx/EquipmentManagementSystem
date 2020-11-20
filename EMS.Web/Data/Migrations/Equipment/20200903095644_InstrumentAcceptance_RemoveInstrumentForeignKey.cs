using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class InstrumentAcceptance_RemoveInstrumentForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstrumentAcceptance_Instrument_InstrumentID",
                table: "InstrumentAcceptance");

            migrationBuilder.DropIndex(
                name: "IX_InstrumentAcceptance_InstrumentID",
                table: "InstrumentAcceptance");

            migrationBuilder.AlterColumn<string>(
                name: "InstrumentID",
                table: "InstrumentAcceptance",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstrumentID",
                table: "InstrumentAcceptance",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAcceptance_InstrumentID",
                table: "InstrumentAcceptance",
                column: "InstrumentID",
                unique: true,
                filter: "[InstrumentID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_InstrumentAcceptance_Instrument_InstrumentID",
                table: "InstrumentAcceptance",
                column: "InstrumentID",
                principalTable: "Instrument",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
