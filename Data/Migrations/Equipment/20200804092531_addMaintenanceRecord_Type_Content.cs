using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class addMaintenanceRecord_Type_Content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.CreateTable(
                name: "MaintenanceType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentPlatform = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Cycle = table.Column<string>(nullable: true),
                    RemindTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: true),
                    Repairer = table.Column<string>(maxLength: 50, nullable: true),
                    Solution = table.Column<string>(maxLength: 100, nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    IsCritical = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 999, nullable: true),
                    Attachment = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: true),
                    UploadTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Repair_MalfunctionWorkOrder_MalfunctionWorkOrderID",
                        column: x => x.MalfunctionWorkOrderID,
                        principalTable: "MalfunctionWorkOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceContent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(maxLength: 50, nullable: true),
                    MaintenanceTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceContent_MaintenanceType_MaintenanceTypeId",
                        column: x => x.MaintenanceTypeId,
                        principalTable: "MaintenanceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceContent_MaintenanceTypeId",
                table: "MaintenanceContent",
                column: "MaintenanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_MalfunctionWorkOrderID",
                table: "Repair",
                column: "MalfunctionWorkOrderID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceContent");

            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "MaintenanceType");

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attachment = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false),
                    MalfunctionWorkOrderID = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: true),
                    Repairer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Solution = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Maintenance_MalfunctionWorkOrder_MalfunctionWorkOrderID",
                        column: x => x.MalfunctionWorkOrderID,
                        principalTable: "MalfunctionWorkOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_MalfunctionWorkOrderID",
                table: "Maintenance",
                column: "MalfunctionWorkOrderID",
                unique: true);
        }
    }
}
