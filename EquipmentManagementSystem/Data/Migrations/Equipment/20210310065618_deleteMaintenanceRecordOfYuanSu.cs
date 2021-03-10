using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class deleteMaintenanceRecordOfYuanSu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceRecordOfYuanSu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaintenanceRecordOfYuanSu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Daily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HalfYearly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Monthly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temporary = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
