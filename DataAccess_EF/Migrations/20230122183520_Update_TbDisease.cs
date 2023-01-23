using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbDisease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TbDiseases",
                newName: "Name_EN");

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                table: "TbDiseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_AR",
                table: "TbDiseases");

            migrationBuilder.RenameColumn(
                name: "Name_EN",
                table: "TbDiseases",
                newName: "Name");
        }
    }
}
