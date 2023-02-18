using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbPneumonia_TbTuberculosis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbPneumonias_AspNetUsers_UserId",
                table: "TbPneumonias");

            migrationBuilder.DropForeignKey(
                name: "FK_TbTuberculosis_AspNetUsers_UserId",
                table: "TbTuberculosis");

            migrationBuilder.DropIndex(
                name: "IX_TbTuberculosis_UserId",
                table: "TbTuberculosis");

            migrationBuilder.DropIndex(
                name: "IX_TbPneumonias_UserId",
                table: "TbPneumonias");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TbTuberculosis");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TbPneumonias");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "TbTuberculosis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "TbPneumonias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbTuberculosis_DoctorId",
                table: "TbTuberculosis",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_TbPneumonias_DoctorId",
                table: "TbPneumonias",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbPneumonias_TbDoctors_DoctorId",
                table: "TbPneumonias",
                column: "DoctorId",
                principalTable: "TbDoctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbTuberculosis_TbDoctors_DoctorId",
                table: "TbTuberculosis",
                column: "DoctorId",
                principalTable: "TbDoctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbPneumonias_TbDoctors_DoctorId",
                table: "TbPneumonias");

            migrationBuilder.DropForeignKey(
                name: "FK_TbTuberculosis_TbDoctors_DoctorId",
                table: "TbTuberculosis");

            migrationBuilder.DropIndex(
                name: "IX_TbTuberculosis_DoctorId",
                table: "TbTuberculosis");

            migrationBuilder.DropIndex(
                name: "IX_TbPneumonias_DoctorId",
                table: "TbPneumonias");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "TbTuberculosis");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "TbPneumonias");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TbTuberculosis",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TbPneumonias",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TbTuberculosis_UserId",
                table: "TbTuberculosis",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TbPneumonias_UserId",
                table: "TbPneumonias",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbPneumonias_AspNetUsers_UserId",
                table: "TbPneumonias",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbTuberculosis_AspNetUsers_UserId",
                table: "TbTuberculosis",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
