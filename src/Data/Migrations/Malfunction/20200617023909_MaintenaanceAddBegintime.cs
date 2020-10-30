using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EquipmentManagementSystem.Migrations
{
    public partial class MaintenaanceAddBegintime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "Maintenance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginTime",
                table: "Maintenance");

        }
    }
}
