using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class RemoveEffectReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectReportFile",
                table: "Validation");

            migrationBuilder.DropColumn(
                name: "EffectReportFileName",
                table: "Validation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "EffectReportFile",
                table: "Validation",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EffectReportFileName",
                table: "Validation",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
