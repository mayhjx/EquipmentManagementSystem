using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class ChangeUsageRecordModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginTimeOfTest",
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
                name: "PressureUnit",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "VacuumDegree",
                table: "UsageRecord");

            migrationBuilder.AlterColumn<string>(
                name: "VacuumDegreeUnit",
                table: "UsageRecord",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "TestSignal",
                table: "UsageRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BlankSignal",
                table: "UsageRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColumnPressureUnit",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HighVacuumDegree",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "LowVacuumDegree",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "SystemOneColumnNumber",
                table: "UsageRecord",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SystemOneColumnPressure",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemTwoColumnNumber",
                table: "UsageRecord",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SystemTwoColumnoPressure",
                table: "UsageRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginTime",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnPressureUnit",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "HighVacuumDegree",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "LowVacuumDegree",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemOneColumnNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemOneColumnPressure",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemTwoColumnNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemTwoColumnoPressure",
                table: "UsageRecord");

            migrationBuilder.AlterColumn<int>(
                name: "VacuumDegreeUnit",
                table: "UsageRecord",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TestSignal",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<string>(
                name: "BlankSignal",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTimeOfTest",
                table: "UsageRecord",
                type: "datetime2",
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

            migrationBuilder.AddColumn<int>(
                name: "PressureUnit",
                table: "UsageRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VacuumDegree",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
