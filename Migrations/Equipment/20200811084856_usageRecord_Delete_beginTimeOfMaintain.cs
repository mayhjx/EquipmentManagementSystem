using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class usageRecord_Delete_beginTimeOfMaintain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginTimeOfMaintain",
                table: "UsageRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTimeOfMaintain",
                table: "UsageRecord",
                type: "datetime2",
                nullable: true);
        }
    }
}
