using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrailLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(nullable: true),
                    DateChanged = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    EntityName = table.Column<string>(nullable: true),
                    PrimaryKeyValue = table.Column<string>(nullable: true),
                    OriginalValue = table.Column<string>(nullable: true),
                    CurrentValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentAcceptances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creator = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    FeasibilityReportFileName = table.Column<string>(nullable: true),
                    FeasibilityReportFilePath = table.Column<string>(nullable: true),
                    ConfigurationListFileName = table.Column<string>(nullable: true),
                    ConfigurationListFilePath = table.Column<string>(nullable: true),
                    PuchaseRequisitionFileName = table.Column<string>(nullable: true),
                    PuchaseRequisitionFilePath = table.Column<string>(nullable: true),
                    IsDemo = table.Column<bool>(nullable: false),
                    InstrumentID = table.Column<string>(nullable: true),
                    InstallationNoteFileName = table.Column<string>(nullable: true),
                    InstallationNoteFilePath = table.Column<string>(nullable: true),
                    EstimatedArrivalDate = table.Column<DateTime>(nullable: true),
                    ArrivalDate = table.Column<DateTime>(nullable: true),
                    IsInventoryComplete = table.Column<bool>(nullable: false),
                    InventoryCertificateFileName = table.Column<string>(nullable: true),
                    InventoryCertificateFilePath = table.Column<string>(nullable: true),
                    InventoryRemark = table.Column<string>(nullable: true),
                    InstallationDate = table.Column<DateTime>(nullable: true),
                    InstallationRemark = table.Column<string>(nullable: true),
                    IsFactoryAcceptance = table.Column<bool>(nullable: false),
                    FactoryAcceptanceDate = table.Column<DateTime>(nullable: true),
                    FactoryAcceptanceReportFileName = table.Column<string>(nullable: true),
                    FactoryAcceptanceReportFilePath = table.Column<string>(nullable: true),
                    ServiceReportFileName = table.Column<string>(nullable: true),
                    ServiceReportFilePath = table.Column<string>(nullable: true),
                    FactoryAcceptanceRemark = table.Column<string>(nullable: true),
                    IsTrainingUseAndMaintenance = table.Column<bool>(nullable: false),
                    TrainingSignInFormFileName = table.Column<string>(nullable: true),
                    TrainingSignInFormFilePath = table.Column<string>(nullable: true),
                    IsSelfBuilt = table.Column<bool>(nullable: false),
                    IsEngineerAssistance = table.Column<bool>(nullable: false),
                    MethodConstructionRemark = table.Column<string>(nullable: true),
                    IsAcceptance = table.Column<bool>(nullable: false),
                    ItemAcceptanceDate = table.Column<DateTime>(nullable: true),
                    EvaluationReportFileName = table.Column<string>(nullable: true),
                    EvaluationReportFilePath = table.Column<string>(nullable: true),
                    EquipmentResumeFileName = table.Column<string>(nullable: true),
                    EquipmentResumeFilePath = table.Column<string>(nullable: true),
                    EquipmentFilesListFileName = table.Column<string>(nullable: true),
                    EquipmentFilesListFilePath = table.Column<string>(nullable: true),
                    EquipmentCertificateFileName = table.Column<string>(nullable: true),
                    EquipmentCertificateFilePath = table.Column<string>(nullable: true),
                    FactoryProductionLicenseFileName = table.Column<string>(nullable: true),
                    FactoryProductionLicenseFilePath = table.Column<string>(nullable: true),
                    BusinessLicenseFileName = table.Column<string>(nullable: true),
                    BusinessLicenseFilePath = table.Column<string>(nullable: true),
                    MedicalDeviceRegistrationCertificateFileName = table.Column<string>(nullable: true),
                    MedicalDeviceRegistrationCertificateFilePath = table.Column<string>(nullable: true),
                    EquipmentCalibrationReportFileName = table.Column<string>(nullable: true),
                    EquipmentCalibrationReportFilePath = table.Column<string>(nullable: true),
                    EquipmentAcceptanceReportFileName = table.Column<string>(nullable: true),
                    EquipmentAcceptanceReportFilePath = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    EquipmentAcceptanceDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentAcceptances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
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
                    Group = table.Column<string>(maxLength: 50, nullable: true),
                    Projects = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentPlatform = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Text = table.Column<string>(maxLength: 100, nullable: true),
                    Translation = table.Column<string>(maxLength: 100, nullable: true),
                    Cycle = table.Column<int>(nullable: false),
                    RemindTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MalfunctionParts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionParts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MalfunctionPhenomenon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionReason", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MalfunctionSolution",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Solution = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionSolution", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    SimpleTestTime = table.Column<DateTime>(nullable: true),
                    ColumnType = table.Column<string>(maxLength: 500, nullable: true),
                    Carrier = table.Column<string>(maxLength: 500, nullable: true),
                    IonSource = table.Column<string>(maxLength: 50, nullable: true),
                    Detector = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asserts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    SourceUnit = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 999, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asserts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Asserts_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calibrations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Unit = table.Column<string>(maxLength: 50, nullable: false),
                    Result = table.Column<int>(nullable: false),
                    Calibrator = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calibrations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Calibrations_Instruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Brand = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Components_Instruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<string>(nullable: true),
                    IP = table.Column<string>(maxLength: 50, nullable: true),
                    Account = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Computer_Instruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MalfunctionWorkOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<string>(nullable: true),
                    Progress = table.Column<int>(nullable: false),
                    Creator = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalfunctionWorkOrder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MalfunctionWorkOrder_Instruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    Content = table.Column<string>(maxLength: 500, nullable: true),
                    Operator = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsageRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    ColumnNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ColumnPressure = table.Column<float>(nullable: true),
                    ColumnTwoNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ColumnTwoPressure = table.Column<float>(nullable: true),
                    PressureUnit = table.Column<int>(nullable: false),
                    BeginTimeOfTest = table.Column<DateTime>(nullable: true),
                    SampleNumber = table.Column<int>(nullable: false),
                    TestNumber = table.Column<int>(nullable: false),
                    VacuumDegree = table.Column<string>(nullable: true),
                    VacuumDegreeUnit = table.Column<int>(nullable: false),
                    BlankSignal = table.Column<string>(nullable: true),
                    TestSignal = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Creator = table.Column<string>(maxLength: 10, nullable: true),
                    Remark = table.Column<string>(maxLength: 999, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageRecords_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsageRecords_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessoriesOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Operator = table.Column<string>(maxLength: 50, nullable: true),
                    Measures = table.Column<string>(maxLength: 999, nullable: true),
                    Result = table.Column<int>(nullable: false)
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
                name: "MalfunctionInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    FoundedTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Part = table.Column<string>(maxLength: 50, nullable: true),
                    Phenomenon = table.Column<string>(maxLength: 50, nullable: true),
                    Reason = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 999, nullable: true),
                    FileName = table.Column<string>(maxLength: 500, nullable: true),
                    FilePath = table.Column<string>(maxLength: 1000, nullable: true),
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
                    FileName = table.Column<string>(maxLength: 500, nullable: true),
                    FilePath = table.Column<string>(maxLength: 1000, nullable: true),
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
                name: "RepairRequest",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalfunctionWorkOrderID = table.Column<int>(nullable: false),
                    FinishedTime = table.Column<DateTime>(nullable: true),
                    PerformanceReportFileName = table.Column<string>(maxLength: 500, nullable: true),
                    PerformanceReportFilePath = table.Column<string>(maxLength: 1000, nullable: true),
                    Summary = table.Column<string>(maxLength: 999, nullable: true),
                    AttachmentName = table.Column<string>(maxLength: 500, nullable: true),
                    AttachmentFilePath = table.Column<string>(maxLength: 1000, nullable: true),
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
                name: "IX_Asserts_InstrumentId",
                table: "Asserts",
                column: "InstrumentId",
                unique: true,
                filter: "[InstrumentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Calibrations_InstrumentID",
                table: "Calibrations",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Components_InstrumentID",
                table: "Components",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Computer_InstrumentID",
                table: "Computer",
                column: "InstrumentID",
                unique: true,
                filter: "[InstrumentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Investigation_MalfunctionWorkOrderID",
                table: "Investigation",
                column: "MalfunctionWorkOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_InstrumentId",
                table: "MaintenanceRecords",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_ProjectId",
                table: "MaintenanceRecords",
                column: "ProjectId");

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
                name: "IX_Projects_GroupId",
                table: "Projects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_MalfunctionWorkOrderID",
                table: "Repair",
                column: "MalfunctionWorkOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequest_MalfunctionWorkOrderID",
                table: "RepairRequest",
                column: "MalfunctionWorkOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecords_InstrumentId",
                table: "UsageRecords",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecords_ProjectId",
                table: "UsageRecords",
                column: "ProjectId");

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
                name: "Asserts");

            migrationBuilder.DropTable(
                name: "AuditTrailLogs");

            migrationBuilder.DropTable(
                name: "Calibrations");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "InstrumentAcceptances");

            migrationBuilder.DropTable(
                name: "Investigation");

            migrationBuilder.DropTable(
                name: "MaintenanceContents");

            migrationBuilder.DropTable(
                name: "MaintenanceRecords");

            migrationBuilder.DropTable(
                name: "MalfunctionInfo");

            migrationBuilder.DropTable(
                name: "MalfunctionParts");

            migrationBuilder.DropTable(
                name: "MalfunctionPhenomenon");

            migrationBuilder.DropTable(
                name: "MalfunctionReason");

            migrationBuilder.DropTable(
                name: "MalfunctionSolution");

            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "RepairRequest");

            migrationBuilder.DropTable(
                name: "UsageRecords");

            migrationBuilder.DropTable(
                name: "Validation");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "MalfunctionWorkOrder");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Instruments");
        }
    }
}
