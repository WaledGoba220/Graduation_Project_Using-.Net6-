using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Add_TbDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TbDoctorId",
                table: "TbSpecialization",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TbDoctorId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbDoctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clinic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Brief = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDoctors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbSpecialization_TbDoctorId",
                table: "TbSpecialization",
                column: "TbDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TbDoctorId",
                table: "AspNetUsers",
                column: "TbDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TbDoctors_TbDoctorId",
                table: "AspNetUsers",
                column: "TbDoctorId",
                principalTable: "TbDoctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbSpecialization_TbDoctors_TbDoctorId",
                table: "TbSpecialization",
                column: "TbDoctorId",
                principalTable: "TbDoctors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TbDoctors_TbDoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TbSpecialization_TbDoctors_TbDoctorId",
                table: "TbSpecialization");

            migrationBuilder.DropTable(
                name: "TbDoctors");

            migrationBuilder.DropIndex(
                name: "IX_TbSpecialization_TbDoctorId",
                table: "TbSpecialization");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TbDoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TbDoctorId",
                table: "TbSpecialization");

            migrationBuilder.DropColumn(
                name: "TbDoctorId",
                table: "AspNetUsers");
        }
    }
}
