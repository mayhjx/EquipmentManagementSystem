using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Validation");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedTime",
                table: "Validation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedTime",
                table: "Validation");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Validation",
                type: "datetime2",
                nullable: true);
        }
    }
}
