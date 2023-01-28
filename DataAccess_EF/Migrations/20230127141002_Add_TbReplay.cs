using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Add_TbReplay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbReplays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Replay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdviceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbReplays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbReplays_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbReplays_TbAdvices_AdviceId",
                        column: x => x.AdviceId,
                        principalTable: "TbAdvices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbReplays_TbComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "TbComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbReplays_AdviceId",
                table: "TbReplays",
                column: "AdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_TbReplays_AppUserId",
                table: "TbReplays",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TbReplays_CommentId",
                table: "TbReplays",
                column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbReplays");
        }
    }
}
