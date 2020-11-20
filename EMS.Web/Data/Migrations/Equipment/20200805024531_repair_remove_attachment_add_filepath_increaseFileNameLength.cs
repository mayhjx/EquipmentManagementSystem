using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class repair_remove_attachment_add_filepath_increaseFileNameLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Repair");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Repair",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Repair",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Repair");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Repair",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Attachment",
                table: "Repair",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
