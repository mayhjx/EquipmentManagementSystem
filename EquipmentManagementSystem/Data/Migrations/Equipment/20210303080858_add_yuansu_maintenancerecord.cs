using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class add_yuansu_maintenancerecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaintenanceRecordOfYuanSu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    BeginTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Daily = table.Column<string>(nullable: true),
                    Monthly = table.Column<string>(nullable: true),
                    HalfYearly = table.Column<string>(nullable: true),
                    Temporary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecordOfYuanSu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecordOfYuanSu_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecordOfYuanSu_InstrumentID",
                table: "MaintenanceRecordOfYuanSu",
                column: "InstrumentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceRecordOfYuanSu");
        }
    }
}
