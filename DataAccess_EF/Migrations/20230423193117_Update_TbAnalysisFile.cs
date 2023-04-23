using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbAnalysisFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalysisFile",
                table: "TbLungTransplants");

            migrationBuilder.DropColumn(
                name: "AnalysisFile",
                table: "TbLungAnalysisFile");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "TbLungAnalysisFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "TbLungAnalysisFile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "TbLungAnalysisFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "TbLungAnalysisFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Size",
                table: "TbLungAnalysisFile",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "TbLungAnalysisFile");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "TbLungAnalysisFile");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "TbLungAnalysisFile");

            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "TbLungAnalysisFile");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "TbLungAnalysisFile");

            migrationBuilder.AddColumn<byte[]>(
                name: "AnalysisFile",
                table: "TbLungTransplants",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "AnalysisFile",
                table: "TbLungAnalysisFile",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
