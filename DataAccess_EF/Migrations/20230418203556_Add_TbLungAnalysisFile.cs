using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Add_TbLungAnalysisFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbLungAnalysisFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalysisFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LungTransplantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbLungAnalysisFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbLungAnalysisFile_TbLungTransplants_LungTransplantId",
                        column: x => x.LungTransplantId,
                        principalTable: "TbLungTransplants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbLungAnalysisFile_LungTransplantId",
                table: "TbLungAnalysisFile",
                column: "LungTransplantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbLungAnalysisFile");
        }
    }
}
