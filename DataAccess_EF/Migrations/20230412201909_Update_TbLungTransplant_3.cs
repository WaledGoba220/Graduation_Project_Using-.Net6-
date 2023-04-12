using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbLungTransplant_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalysisFile",
                table: "TbLungTransplants");

            migrationBuilder.DropColumn(
                name: "ChestRayImage",
                table: "TbLungTransplants");

            migrationBuilder.DropColumn(
                name: "NationalImage",
                table: "TbLungTransplants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AnalysisFile",
                table: "TbLungTransplants",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "ChestRayImage",
                table: "TbLungTransplants",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "NationalImage",
                table: "TbLungTransplants",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
