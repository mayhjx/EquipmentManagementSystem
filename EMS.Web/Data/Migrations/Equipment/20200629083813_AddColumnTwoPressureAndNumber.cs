using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class AddColumnTwoPressureAndNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColumnNumber",
                table: "UsageRecord",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColumnTwoNumber",
                table: "UsageRecord",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ColumnTwoPressure",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SimpleTestTime",
                table: "Project",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ColumnType",
                table: "Project",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnTwoNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnTwoPressure",
                table: "UsageRecord");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SimpleTestTime",
                table: "Project",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColumnType",
                table: "Project",
                type: "nvarchar(max)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
