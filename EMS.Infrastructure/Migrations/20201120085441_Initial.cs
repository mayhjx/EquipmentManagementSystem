using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcceptanceUploadFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptanceUploadFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(maxLength: 20, nullable: false),
                    Platform = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    StartUsingDate = table.Column<DateTime>(nullable: false),
                    CalibrationCycle = table.Column<int>(nullable: false),
                    MetrologicalCharacteristics = table.Column<string>(maxLength: 10, nullable: false),
                    Status = table.Column<string>(maxLength: 10, nullable: false),
                    Location = table.Column<string>(maxLength: 100, nullable: false),
                    PersonInCharge = table.Column<string>(maxLength: 10, nullable: false),
                    NewSystemCode = table.Column<string>(maxLength: 20, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instruments_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Acceptances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentNumber = table.Column<string>(maxLength: 20, nullable: false),
                    InstrumentId = table.Column<int>(nullable: false),
                    Operator = table.Column<string>(maxLength: 10, nullable: true),
                    InstallationNoteId = table.Column<int>(nullable: true),
                    EstimatedArrivalDate = table.Column<DateTime>(nullable: true),
                    ArrivalDate = table.Column<DateTime>(nullable: true),
                    IsInventoryComplete = table.Column<bool>(nullable: false),
                    InventoryCertificateId = table.Column<int>(nullable: true),
                    InventoryRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    InstallationDate = table.Column<DateTime>(nullable: true),
                    InstallationRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    IsFactoryAcceptance = table.Column<bool>(nullable: false),
                    FactoryAcceptanceDate = table.Column<DateTime>(nullable: true),
                    FactoryAcceptanceReportId = table.Column<int>(nullable: true),
                    ServiceReportId = table.Column<int>(nullable: true),
                    FactoryAcceptanceRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    IsTrainingUseAndMaintenance = table.Column<bool>(nullable: false),
                    TrainingSignInFormId = table.Column<int>(nullable: true),
                    IsSelfBuilt = table.Column<bool>(nullable: false),
                    IsEngineerAssistance = table.Column<bool>(nullable: false),
                    MethodConstructionRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    IsAcceptance = table.Column<bool>(nullable: false),
                    ItemAcceptanceDate = table.Column<DateTime>(nullable: true),
                    EvaluationReportId = table.Column<int>(nullable: true),
                    EquipmentResumeId = table.Column<int>(nullable: true),
                    EquipmentFilesListId = table.Column<int>(nullable: true),
                    EquipmentCertificateId = table.Column<int>(nullable: true),
                    FactoryProductionLicenseId = table.Column<int>(nullable: true),
                    BusinessLicenseId = table.Column<int>(nullable: true),
                    MedicalDeviceRegistrationCertificateId = table.Column<int>(nullable: true),
                    EquipmentCalibrationReportId = table.Column<int>(nullable: true),
                    EquipmentAcceptanceReportId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acceptances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_BusinessLicenseId",
                        column: x => x.BusinessLicenseId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_EquipmentAcceptanceReportId",
                        column: x => x.EquipmentAcceptanceReportId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_EquipmentCalibrationReportId",
                        column: x => x.EquipmentCalibrationReportId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_EquipmentCertificateId",
                        column: x => x.EquipmentCertificateId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_EquipmentFilesListId",
                        column: x => x.EquipmentFilesListId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_EquipmentResumeId",
                        column: x => x.EquipmentResumeId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_EvaluationReportId",
                        column: x => x.EvaluationReportId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_FactoryAcceptanceReportId",
                        column: x => x.FactoryAcceptanceReportId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_FactoryProductionLicenseId",
                        column: x => x.FactoryProductionLicenseId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_InstallationNoteId",
                        column: x => x.InstallationNoteId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_InventoryCertificateId",
                        column: x => x.InventoryCertificateId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_MedicalDeviceRegistrationCertificateId",
                        column: x => x.MedicalDeviceRegistrationCertificateId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_ServiceReportId",
                        column: x => x.ServiceReportId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acceptances_AcceptanceUploadFile_TrainingSignInFormId",
                        column: x => x.TrainingSignInFormId,
                        principalTable: "AcceptanceUploadFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calibrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentNumber = table.Column<string>(maxLength: 20, nullable: false),
                    InstrumentId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Unit = table.Column<string>(maxLength: 50, nullable: false),
                    Result = table.Column<string>(maxLength: 10, nullable: false),
                    Operator = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calibrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calibrations_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentNumber = table.Column<string>(maxLength: 20, nullable: false),
                    InstrumentId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Model = table.Column<string>(maxLength: 100, nullable: false),
                    Brand = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    InstrumentId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Projects_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsageRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentNumber = table.Column<string>(maxLength: 20, nullable: true),
                    GroupName = table.Column<string>(maxLength: 50, nullable: true),
                    ProjectName = table.Column<string>(maxLength: 50, nullable: true),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    MobilePhase = table.Column<string>(maxLength: 1000, nullable: true),
                    ColumnType = table.Column<string>(maxLength: 1000, nullable: true),
                    Column = table.Column<string>(maxLength: 1000, nullable: true),
                    VacuumDegree = table.Column<string>(maxLength: 1000, nullable: true),
                    Blank = table.Column<string>(maxLength: 1000, nullable: true),
                    Test = table.Column<string>(maxLength: 1000, nullable: true),
                    ClinicSampleNumber = table.Column<int>(nullable: false),
                    BatchSampleNumber = table.Column<int>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    User = table.Column<string>(maxLength: 10, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    InstrumentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageRecords_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationUploadFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalibrationId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationUploadFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationUploadFile_Calibrations_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "Calibrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_BusinessLicenseId",
                table: "Acceptances",
                column: "BusinessLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_EquipmentAcceptanceReportId",
                table: "Acceptances",
                column: "EquipmentAcceptanceReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_EquipmentCalibrationReportId",
                table: "Acceptances",
                column: "EquipmentCalibrationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_EquipmentCertificateId",
                table: "Acceptances",
                column: "EquipmentCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_EquipmentFilesListId",
                table: "Acceptances",
                column: "EquipmentFilesListId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_EquipmentResumeId",
                table: "Acceptances",
                column: "EquipmentResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_EvaluationReportId",
                table: "Acceptances",
                column: "EvaluationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_FactoryAcceptanceReportId",
                table: "Acceptances",
                column: "FactoryAcceptanceReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_FactoryProductionLicenseId",
                table: "Acceptances",
                column: "FactoryProductionLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_InstallationNoteId",
                table: "Acceptances",
                column: "InstallationNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_InstrumentId",
                table: "Acceptances",
                column: "InstrumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_InventoryCertificateId",
                table: "Acceptances",
                column: "InventoryCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_MedicalDeviceRegistrationCertificateId",
                table: "Acceptances",
                column: "MedicalDeviceRegistrationCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_ServiceReportId",
                table: "Acceptances",
                column: "ServiceReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptances_TrainingSignInFormId",
                table: "Acceptances",
                column: "TrainingSignInFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Calibrations_InstrumentId",
                table: "Calibrations",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationUploadFile_CalibrationId",
                table: "CalibrationUploadFile",
                column: "CalibrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_InstrumentId",
                table: "Components",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_GroupId",
                table: "Instruments",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GroupId",
                table: "Projects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_InstrumentId",
                table: "Projects",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecords_InstrumentId",
                table: "UsageRecords",
                column: "InstrumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acceptances");

            migrationBuilder.DropTable(
                name: "CalibrationUploadFile");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "UsageRecords");

            migrationBuilder.DropTable(
                name: "AcceptanceUploadFile");

            migrationBuilder.DropTable(
                name: "Calibrations");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
