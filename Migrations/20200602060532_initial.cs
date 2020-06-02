using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Platform = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    StartUsingDate = table.Column<DateTime>(nullable: false),
                    CalibrationCycle = table.Column<int>(nullable: false),
                    MetrologicalCharacteristics = table.Column<string>(maxLength: 10, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    Principal = table.Column<string>(maxLength: 10, nullable: false),
                    Remark = table.Column<string>(maxLength: 999, nullable: true),
                    NewSystemCode = table.Column<string>(maxLength: 10, nullable: true),
                    ProjectTeamName = table.Column<string>(maxLength: 50, nullable: true),
                    Projects = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MalfunctionPart",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionPart", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MalfunctionPhenomenon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Phenomenon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionPhenomenon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MalfunctionReason",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionReason", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Assert",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstrumentId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    SourceUnit = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 999, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assert", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assert_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calibration",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstrumentID = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Unit = table.Column<string>(maxLength: 50, nullable: false),
                    Result = table.Column<int>(nullable: false),
                    Calibrator = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calibration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Calibration_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstrumentID = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Brand = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Component_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstrumentID = table.Column<string>(nullable: true),
                    IP = table.Column<string>(maxLength: 50, nullable: true),
                    Account = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Computer_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MalfunctionWorkOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstrumentID = table.Column<string>(nullable: true),
                    Progress = table.Column<int>(nullable: false),
                    Creator = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionWorkOrder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MalfunctionWorkOrder_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessoriesOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    PlaceTime = table.Column<DateTime>(nullable: true),
                    ArrivalTime = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(maxLength: 999, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessoriesOrder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccessoriesOrder_MalfunctionWorkOrder_MalfunctionWorkOrderID",
                        column: x => x.MalfunctionWorkOrderID,
                        principalTable: "MalfunctionWorkOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investigation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Operator = table.Column<string>(maxLength: 50, nullable: true),
                    Measures = table.Column<string>(maxLength: 999, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Investigation_MalfunctionWorkOrder_MalfunctionWorkOrderID",
                        column: x => x.MalfunctionWorkOrderID,
                        principalTable: "MalfunctionWorkOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    Repairer = table.Column<string>(maxLength: 50, nullable: true),
                    Solution = table.Column<string>(maxLength: 999, nullable: true),
                    IsCritical = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 999, nullable: true),
                    Attachment = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: true),
                    UploadTime = table.Column<DateTime>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "MalfunctionInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    FoundedTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Part = table.Column<string>(maxLength: 50, nullable: true),
                    Phenomenon = table.Column<string>(maxLength: 50, nullable: true),
                    Reason = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 999, nullable: true),
                    Attachment = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: true),
                    UploadTime = table.Column<DateTime>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MalfunctionInfo_MalfunctionWorkOrder_MalfunctionWorkOrderID",
                        column: x => x.MalfunctionWorkOrderID,
                        principalTable: "MalfunctionWorkOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairRequest",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    RequestTime = table.Column<DateTime>(nullable: true),
                    BookingsTime = table.Column<DateTime>(nullable: true),
                    Fixer = table.Column<string>(maxLength: 50, nullable: true),
                    Engineer = table.Column<string>(maxLength: 50, nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RepairRequest_MalfunctionWorkOrder_MalfunctionWorkOrderID",
                        column: x => x.MalfunctionWorkOrderID,
                        principalTable: "MalfunctionWorkOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Validation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    PerformanceReportFileName = table.Column<string>(maxLength: 100, nullable: true),
                    PerformanceReportFile = table.Column<byte[]>(nullable: true),
                    EffectReportFileName = table.Column<string>(maxLength: 100, nullable: true),
                    EffectReportFile = table.Column<byte[]>(nullable: true),
                    Summary = table.Column<string>(maxLength: 999, nullable: true),
                    Attachment = table.Column<byte[]>(nullable: true),
                    AttachmentName = table.Column<string>(maxLength: 100, nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Validation_MalfunctionWorkOrder_MalfunctionWorkOrderID",
                        column: x => x.MalfunctionWorkOrderID,
                        principalTable: "MalfunctionWorkOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessoriesOrder_MalfunctionWorkOrderID",
                table: "AccessoriesOrder",
                column: "MalfunctionWorkOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assert_InstrumentId",
                table: "Assert",
                column: "InstrumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calibration_InstrumentID",
                table: "Calibration",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Component_InstrumentID",
                table: "Component",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Computer_InstrumentID",
                table: "Computer",
                column: "InstrumentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investigation_MalfunctionWorkOrderID",
                table: "Investigation",
                column: "MalfunctionWorkOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_MalfunctionWorkOrderID",
                table: "Maintenance",
                column: "MalfunctionWorkOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalfunctionInfo_MalfunctionWorkOrderID",
                table: "MalfunctionInfo",
                column: "MalfunctionWorkOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalfunctionWorkOrder_InstrumentID",
                table: "MalfunctionWorkOrder",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequest_MalfunctionWorkOrderID",
                table: "RepairRequest",
                column: "MalfunctionWorkOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Validation_MalfunctionWorkOrderID",
                table: "Validation",
                column: "MalfunctionWorkOrderID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessoriesOrder");

            migrationBuilder.DropTable(
                name: "Assert");

            migrationBuilder.DropTable(
                name: "Calibration");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "Investigation");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "MalfunctionInfo");

            migrationBuilder.DropTable(
                name: "MalfunctionPart");

            migrationBuilder.DropTable(
                name: "MalfunctionPhenomenon");

            migrationBuilder.DropTable(
                name: "MalfunctionReason");

            migrationBuilder.DropTable(
                name: "RepairRequest");

            migrationBuilder.DropTable(
                name: "Validation");

            migrationBuilder.DropTable(
                name: "MalfunctionWorkOrder");

            migrationBuilder.DropTable(
                name: "Instrument");
        }
    }
}
