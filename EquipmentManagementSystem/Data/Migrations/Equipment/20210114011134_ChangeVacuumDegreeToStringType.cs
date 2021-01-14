using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class ChangeVacuumDegreeToStringType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "Remark",
            //    table: "UsageRecord",
            //    maxLength: 20,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(999)",
            //    oldMaxLength: 999,
            //    oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LowVacuumDegree",
                table: "UsageRecord",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HighVacuumDegree",
                table: "UsageRecord",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "Remark",
            //    table: "UsageRecord",
            //    type: "nvarchar(999)",
            //    maxLength: 999,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 20,
            //    oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "LowVacuumDegree",
                table: "UsageRecord",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "HighVacuumDegree",
                table: "UsageRecord",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
