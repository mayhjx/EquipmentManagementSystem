using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class AddInstrumentAcceptanceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstrumentAcceptance",
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
                    FactoryAcceptanceCertificateFileName = table.Column<string>(nullable: true),
                    FactoryAcceptanceCertificateFilePath = table.Column<string>(nullable: true),
                    ServiceReportFileName = table.Column<string>(nullable: true),
                    ServiceReportFilePath = table.Column<string>(nullable: true),
                    IsTrainingUseAndMaintenance = table.Column<bool>(nullable: false),
                    TrainingSignInFormFileName = table.Column<string>(nullable: true),
                    TrainingSignInFormFilePath = table.Column<string>(nullable: true),
                    IsSelfBuilt = table.Column<bool>(nullable: false),
                    IsEngineerAssistance = table.Column<bool>(nullable: false),
                    MethodConstructionRemark = table.Column<string>(nullable: true),
                    IsAcceptance = table.Column<bool>(nullable: false),
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
                    EquipmentAcceptanceReportFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentAcceptance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentAcceptance_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAcceptance_InstrumentID",
                table: "InstrumentAcceptance",
                column: "InstrumentID",
                unique: true,
                filter: "[InstrumentID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentAcceptance");
        }
    }
}
