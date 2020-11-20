using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class InstrumentAcceptance_AddRemarkAndEquipmentcontetAcceptanceDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptanceDate",
                table: "InstrumentAcceptance");

            migrationBuilder.DropColumn(
                name: "FactoryAcceptanceCertificateFileName",
                table: "InstrumentAcceptance");

            migrationBuilder.DropColumn(
                name: "FactoryAcceptanceCertificateFilePath",
                table: "InstrumentAcceptance");

            migrationBuilder.AddColumn<DateTime>(
                name: "EquipmentAcceptanceDate",
                table: "InstrumentAcceptance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactoryAcceptanceRemark",
                table: "InstrumentAcceptance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactoryAcceptanceReportFileName",
                table: "InstrumentAcceptance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactoryAcceptanceReportFilePath",
                table: "InstrumentAcceptance",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ItemAcceptanceDate",
                table: "InstrumentAcceptance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquipmentAcceptanceDate",
                table: "InstrumentAcceptance");

            migrationBuilder.DropColumn(
                name: "FactoryAcceptanceRemark",
                table: "InstrumentAcceptance");

            migrationBuilder.DropColumn(
                name: "FactoryAcceptanceReportFileName",
                table: "InstrumentAcceptance");

            migrationBuilder.DropColumn(
                name: "FactoryAcceptanceReportFilePath",
                table: "InstrumentAcceptance");

            migrationBuilder.DropColumn(
                name: "ItemAcceptanceDate",
                table: "InstrumentAcceptance");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptanceDate",
                table: "InstrumentAcceptance",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactoryAcceptanceCertificateFileName",
                table: "InstrumentAcceptance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactoryAcceptanceCertificateFilePath",
                table: "InstrumentAcceptance",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
