using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EquipmentManagementSystem.Migrations.Equipment
{
    public partial class addgroup_project_usagerecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });



            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "UsageRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    SampleNumber = table.Column<int>(nullable: false),
                    TestTime = table.Column<DateTime>(nullable: false),
                    InstrumentId = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageRecord_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsageRecord_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_UsageRecord_InstrumentId",
                table: "UsageRecord",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecord_ProjectId",
                table: "UsageRecord",
                column: "ProjectId");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "Group");


            migrationBuilder.DropTable(
                name: "UsageRecord");


            migrationBuilder.DropTable(
                name: "Project");

        }
    }
}
