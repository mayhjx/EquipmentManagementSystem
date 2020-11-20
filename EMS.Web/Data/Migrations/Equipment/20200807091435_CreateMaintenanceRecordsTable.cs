using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class CreateMaintenanceRecordsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaintenanceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(maxLength: 50, nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    BeginTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 20, nullable: true),
                    Content = table.Column<string>(maxLength: 100, nullable: true),
                    Operator = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_InstrumentId",
                table: "MaintenanceRecords",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_ProjectId",
                table: "MaintenanceRecords",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceRecords");
        }
    }
}
