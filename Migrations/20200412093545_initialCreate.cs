using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectTeam",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeam", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now', 'localtime')"),
                    ModifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now', 'localtime')"),
                    Platform = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    StartUsingDate = table.Column<DateTime>(nullable: false),
                    CalibrationCycle = table.Column<int>(nullable: false),
                    MetrologicalCharacteristics = table.Column<string>(maxLength: 10, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    Principal = table.Column<string>(maxLength: 10, nullable: false),
                    Remark = table.Column<string>(maxLength: 999, nullable: true),
                    NewSystemCode = table.Column<string>(maxLength: 50, nullable: true),
                    ProjectTeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Instrument_ProjectTeam_ProjectTeamID",
                        column: x => x.ProjectTeamID,
                        principalTable: "ProjectTeam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assert",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    SourceUnit = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    instrumentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assert", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assert_Instrument_instrumentId",
                        column: x => x.instrumentId,
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
                    Result = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: false),
                    Calibrator = table.Column<string>(nullable: false)
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
                    instrumentID = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Component_Instrument_instrumentID",
                        column: x => x.instrumentID,
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
                    IP = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    InstrumentID = table.Column<string>(nullable: true)
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
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    instrumentID = table.Column<string>(nullable: true),
                    ProjectTeamName = table.Column<string>(nullable: false),
                    projectTeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Project_Instrument_instrumentID",
                        column: x => x.instrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_ProjectTeam_projectTeamID",
                        column: x => x.projectTeamID,
                        principalTable: "ProjectTeam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Malfunction",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    instrumentID = table.Column<string>(nullable: true),
                    componentID = table.Column<int>(nullable: false),
                    Category = table.Column<string>(maxLength: 50, nullable: false),
                    Problem = table.Column<string>(maxLength: 50, nullable: false),
                    Reason = table.Column<string>(maxLength: 50, nullable: false),
                    FoundTime = table.Column<string>(nullable: true),
                    StartTrackTime = table.Column<string>(nullable: true),
                    ReportTime = table.Column<string>(nullable: true),
                    FollowUpPeople = table.Column<string>(maxLength: 10, nullable: false),
                    DebuggingTime = table.Column<string>(nullable: true),
                    PlaceOrderTime = table.Column<string>(nullable: true),
                    AccessoriesArrivalTime = table.Column<string>(nullable: true),
                    EngineerArrivalTime = table.Column<string>(nullable: true),
                    Solutions = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Malfunction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Malfunction_Component_componentID",
                        column: x => x.componentID,
                        principalTable: "Component",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Malfunction_Instrument_instrumentID",
                        column: x => x.instrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assert_instrumentId",
                table: "Assert",
                column: "instrumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calibration_InstrumentID",
                table: "Calibration",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Component_instrumentID",
                table: "Component",
                column: "instrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Computer_InstrumentID",
                table: "Computer",
                column: "InstrumentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_ProjectTeamID",
                table: "Instrument",
                column: "ProjectTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Malfunction_componentID",
                table: "Malfunction",
                column: "componentID");

            migrationBuilder.CreateIndex(
                name: "IX_Malfunction_instrumentID",
                table: "Malfunction",
                column: "instrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_instrumentID",
                table: "Project",
                column: "instrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_projectTeamID",
                table: "Project",
                column: "projectTeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assert");

            migrationBuilder.DropTable(
                name: "Calibration");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "Malfunction");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "Instrument");

            migrationBuilder.DropTable(
                name: "ProjectTeam");
        }
    }
}
