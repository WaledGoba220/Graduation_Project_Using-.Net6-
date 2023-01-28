using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Add_TbComment_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdviceId",
                table: "TbComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "TbComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TbComments_AdviceId",
                table: "TbComments",
                column: "AdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_TbComments_AppUserId",
                table: "TbComments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbComments_AspNetUsers_AppUserId",
                table: "TbComments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbComments_TbAdvices_AdviceId",
                table: "TbComments",
                column: "AdviceId",
                principalTable: "TbAdvices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbComments_AspNetUsers_AppUserId",
                table: "TbComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TbComments_TbAdvices_AdviceId",
                table: "TbComments");

            migrationBuilder.DropIndex(
                name: "IX_TbComments_AdviceId",
                table: "TbComments");

            migrationBuilder.DropIndex(
                name: "IX_TbComments_AppUserId",
                table: "TbComments");

            migrationBuilder.DropColumn(
                name: "AdviceId",
                table: "TbComments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "TbComments");
        }
    }
}
