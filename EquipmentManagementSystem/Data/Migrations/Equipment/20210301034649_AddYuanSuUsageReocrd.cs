using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentManagementSystem.Migrations
{
    public partial class AddYuanSuUsageReocrd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsageRecordOfYuanSu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    BeginTime = table.Column<DateTime>(nullable: true),
                    SensitivityCo = table.Column<double>(nullable: false),
                    SensitivityCoRSD = table.Column<double>(nullable: false),
                    SensitivityY = table.Column<double>(nullable: false),
                    SensitivityYRSD = table.Column<double>(nullable: false),
                    SensitivityTi = table.Column<double>(nullable: false),
                    SensitivityTiRSD = table.Column<double>(nullable: false),
                    MassResolutionCo = table.Column<double>(nullable: false),
                    MassResolutionY = table.Column<double>(nullable: false),
                    MassResolutionTi = table.Column<double>(nullable: false),
                    MassAxisCo = table.Column<double>(nullable: false),
                    MassAxisY = table.Column<double>(nullable: false),
                    MassAxisTi = table.Column<double>(nullable: false),
                    OxideOfCe = table.Column<double>(nullable: false),
                    DoubleChargeOfCe = table.Column<double>(nullable: false),
                    Mass78 = table.Column<double>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageRecordOfYuanSu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageRecordOfYuanSu_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecordOfYuanSu_InstrumentID",
                table: "UsageRecordOfYuanSu",
                column: "InstrumentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsageRecordOfYuanSu");
        }
    }
}
