using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class validation_remove_byte_add_filepath_increaseFileNameLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Validation");

            migrationBuilder.DropColumn(
                name: "PerformanceReportFile",
                table: "Validation");

            migrationBuilder.AlterColumn<string>(
                name: "PerformanceReportFileName",
                table: "Validation",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AttachmentName",
                table: "Validation",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentFilePath",
                table: "Validation",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerformanceReportFilePath",
                table: "Validation",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentFilePath",
                table: "Validation");

            migrationBuilder.DropColumn(
                name: "PerformanceReportFilePath",
                table: "Validation");

            migrationBuilder.AlterColumn<string>(
                name: "PerformanceReportFileName",
                table: "Validation",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AttachmentName",
                table: "Validation",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Attachment",
                table: "Validation",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PerformanceReportFile",
                table: "Validation",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
