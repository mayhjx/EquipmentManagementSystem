using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class ChangeProjectName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsageRecord_Project_ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "UsageRecord");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "UsageRecord",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "UsageRecord",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "UsageRecord",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord",
                column: "ProjectId");

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
