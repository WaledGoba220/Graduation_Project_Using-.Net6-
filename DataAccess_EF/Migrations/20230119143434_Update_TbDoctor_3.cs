using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbDoctor_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TbDoctors_TbDoctorId",
                table: "AspNetUsers");


            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TbDoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TbDoctorId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "TbDoctors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TbDoctors_AppUserId",
                table: "TbDoctors",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TbDoctors_SpecializationId",
                table: "TbDoctors",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbDoctors_AspNetUsers_AppUserId",
                table: "TbDoctors",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbDoctors_TbSpecialization_SpecializationId",
                table: "TbDoctors",
                column: "SpecializationId",
                principalTable: "TbSpecialization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbDoctors_AspNetUsers_AppUserId",
                table: "TbDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_TbDoctors_TbSpecialization_SpecializationId",
                table: "TbDoctors");

            migrationBuilder.DropIndex(
                name: "IX_TbDoctors_AppUserId",
                table: "TbDoctors");

            migrationBuilder.DropIndex(
                name: "IX_TbDoctors_SpecializationId",
                table: "TbDoctors");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "TbDoctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "TbDoctorId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbDoctorTbSpecialization",
                columns: table => new
                {
                    DoctorsId = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDoctorTbSpecialization", x => new { x.DoctorsId, x.SpecializationId });
                    table.ForeignKey(
                        name: "FK_TbDoctorTbSpecialization_TbDoctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "TbDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbDoctorTbSpecialization_TbSpecialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "TbSpecialization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TbDoctorId",
                table: "AspNetUsers",
                column: "TbDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_TbDoctorTbSpecialization_SpecializationId",
                table: "TbDoctorTbSpecialization",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TbDoctors_TbDoctorId",
                table: "AspNetUsers",
                column: "TbDoctorId",
                principalTable: "TbDoctors",
                principalColumn: "Id");
        }
    }
}
