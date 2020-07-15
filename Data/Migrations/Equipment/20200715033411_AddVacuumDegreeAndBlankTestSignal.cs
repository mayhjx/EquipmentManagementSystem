using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class AddVacuumDegreeAndBlankTestSignal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PressureUnit",
                table: "UsageRecord",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BlankSignal",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TestSignal",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VacuumDegree",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VacuumDegreeUnit",
                table: "UsageRecord",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlankSignal",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "TestSignal",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "VacuumDegree",
                table: "UsageRecord");

            migrationBuilder.DropColumn(
                name: "VacuumDegreeUnit",
                table: "UsageRecord");

            migrationBuilder.AlterColumn<string>(
                name: "PressureUnit",
                table: "UsageRecord",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 10);
        }
    }
}
