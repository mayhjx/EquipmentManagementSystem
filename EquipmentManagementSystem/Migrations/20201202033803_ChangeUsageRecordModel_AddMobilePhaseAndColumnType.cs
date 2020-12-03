using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class ChangeUsageRecordModel_AddMobilePhaseAndColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestNumber",
                table: "UsageRecord");

            migrationBuilder.AlterColumn<string>(
                name: "SystemTwoColumnNumber",
                table: "UsageRecord",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SystemOneColumnNumber",
                table: "UsageRecord",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Creator",
                table: "UsageRecord",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhase",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColumnType",
                table: "UsageRecord",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemOneTestNumber",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemTwoTestNumber",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobilePhase",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "ColumnType",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemOneTestNumber",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "SystemTwoTestNumber",
                table: "UsageRecord");

            migrationBuilder.AlterColumn<string>(
                name: "SystemTwoColumnNumber",
                table: "UsageRecord",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SystemOneColumnNumber",
                table: "UsageRecord",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Creator",
                table: "UsageRecord",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestNumber",
                table: "UsageRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
