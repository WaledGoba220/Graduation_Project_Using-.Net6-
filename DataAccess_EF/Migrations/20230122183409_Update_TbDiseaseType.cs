using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbDiseaseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TbDiseaseTypes",
                newName: "Name_EN");

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                table: "TbDiseaseTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_AR",
                table: "TbDiseaseTypes");

            migrationBuilder.RenameColumn(
                name: "Name_EN",
                table: "TbDiseaseTypes",
                newName: "Name");
        }
    }
}
