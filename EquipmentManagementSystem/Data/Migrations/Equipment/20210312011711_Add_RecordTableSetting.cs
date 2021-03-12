using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class Add_RecordTableSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordTableSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<string>(nullable: true),
                    UsageRecordTableChineseTitle = table.Column<string>(nullable: true),
                    UsageRecordTableEnglishTitle = table.Column<string>(nullable: true),
                    UsageRecordTableNumber = table.Column<string>(nullable: true),
                    MaintenanceRecordTableChineseTitle = table.Column<string>(nullable: true),
                    MaintenanceRecordTableEnglishTitle = table.Column<string>(nullable: true),
                    MaintenanceRecordTableNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTableSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordTableSettings_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordTableSettings_InstrumentID",
                table: "RecordTableSettings",
                column: "InstrumentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordTableSettings");
        }
    }
}
