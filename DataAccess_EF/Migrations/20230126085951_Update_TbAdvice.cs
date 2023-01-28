using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbAdvice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "TbAdvices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TbAdvices_AppUserId",
                table: "TbAdvices",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbAdvices_AspNetUsers_AppUserId",
                table: "TbAdvices",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbAdvices_AspNetUsers_AppUserId",
                table: "TbAdvices");

            migrationBuilder.DropIndex(
                name: "IX_TbAdvices_AppUserId",
                table: "TbAdvices");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "TbAdvices");
        }
    }
}
