using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class changeUsageRecordmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsageRecord_Instrument_InstrumentId",
                table: "UsageRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageRecord_Project_ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropIndex(
                name: "IX_UsageRecord_InstrumentId",
                table: "UsageRecord");

            migrationBuilder.DropIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "BeginTimeOfTest",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "BlankSignal",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnPressure",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnTwoNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnTwoPressure",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "PressureUnit",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SampleNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "TestNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "TestSignal",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "VacuumDegreeUnit",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "VacuumDegree",
                table: "UsageRecord",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "UsageRecord",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(999)",
                oldMaxLength: 999,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstrumentId",
                table: "UsageRecord",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BatchSampleNumber",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "UsageRecord",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Blank",
                table: "UsageRecord",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClinicSampleNumber",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Column",
                table: "UsageRecord",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColumnType",
                table: "UsageRecord",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhase",
                table: "UsageRecord",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "UsageRecord",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "UsageRecord",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IonSource",
                table: "Project",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Detector",
                table: "Project",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColumnType",
                table: "Project",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhase",
                table: "Project",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfColumn",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTest",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVacuumDegree",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfColumn",
                table: "Project",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfTest",
                table: "Project",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfVacuumDegree",
                table: "Project",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchSampleNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "BeginTime",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "Blank",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ClinicSampleNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "Column",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnType",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "MobilePhase",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "Test",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "User",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "MobilePhase",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "NumberOfColumn",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "NumberOfTest",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "NumberOfVacuumDegree",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UnitOfColumn",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UnitOfTest",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UnitOfVacuumDegree",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "VacuumDegree",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "UsageRecord",
                type: "nvarchar(999)",
                maxLength: 999,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstrumentId",
                table: "UsageRecord",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTimeOfTest",
                table: "UsageRecord",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlankSignal",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColumnNumber",
                table: "UsageRecord",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ColumnPressure",
                table: "UsageRecord",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColumnTwoNumber",
                table: "UsageRecord",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ColumnTwoPressure",
                table: "UsageRecord",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "UsageRecord",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PressureUnit",
                table: "UsageRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "UsageRecord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SampleNumber",
                table: "UsageRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TestNumber",
                table: "UsageRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TestSignal",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VacuumDegreeUnit",
                table: "UsageRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "IonSource",
                table: "Project",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Detector",
                table: "Project",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColumnType",
                table: "Project",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Carrier",
                table: "Project",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecord_InstrumentId",
                table: "UsageRecord",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRecord_Instrument_InstrumentId",
                table: "UsageRecord",
                column: "InstrumentId",
                principalTable: "Instrument",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRecord_Project_ProjectId",
                table: "UsageRecord",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
