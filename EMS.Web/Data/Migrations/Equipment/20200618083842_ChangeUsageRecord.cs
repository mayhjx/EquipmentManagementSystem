using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class ChangeUsageRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginTime",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "TestTime",
                table: "UsageRecord");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTimeOfMaintain",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTimeOfTest",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "UsageRecord",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestNumber",
                table: "UsageRecord",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_GroupId",
                table: "Project",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Group_GroupId",
                table: "Project",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRecord_Project_ProjectId",
                table: "UsageRecord",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Group_GroupId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageRecord_Project_ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropIndex(
                name: "IX_Project_GroupId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "BeginTimeOfMaintain",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "BeginTimeOfTest",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "TestNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Project");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "UsageRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TestTime",
                table: "UsageRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
