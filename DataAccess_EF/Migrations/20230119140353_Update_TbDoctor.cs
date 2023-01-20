using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TbDoctors",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "SpecialId",
                table: "TbDoctors",
                newName: "SpecializationlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecializationlId",
                table: "TbDoctors",
                newName: "SpecialId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "TbDoctors",
                newName: "UserId");
        }
    }
}
