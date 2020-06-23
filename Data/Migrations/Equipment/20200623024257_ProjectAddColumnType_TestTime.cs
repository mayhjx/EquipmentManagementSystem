using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class ProjectAddColumnType_TestTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColumnType",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SimpleTestTime",
                table: "Project",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnType",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "SimpleTestTime",
                table: "Project");
        }
    }
}
