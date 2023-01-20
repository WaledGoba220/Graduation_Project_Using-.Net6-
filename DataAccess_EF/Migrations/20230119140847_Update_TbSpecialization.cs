using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Update_TbSpecialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbSpecialization_TbDoctors_TbDoctorId",
                table: "TbSpecialization");

            migrationBuilder.DropIndex(
                name: "IX_TbSpecialization_TbDoctorId",
                table: "TbSpecialization");

            migrationBuilder.DropColumn(
                name: "TbDoctorId",
                table: "TbSpecialization");

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
                name: "IX_TbDoctorTbSpecialization_SpecializationId",
                table: "TbDoctorTbSpecialization",
                column: "SpecializationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbDoctorTbSpecialization");

            migrationBuilder.AddColumn<int>(
                name: "TbDoctorId",
                table: "TbSpecialization",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbSpecialization_TbDoctorId",
                table: "TbSpecialization",
                column: "TbDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbSpecialization_TbDoctors_TbDoctorId",
                table: "TbSpecialization",
                column: "TbDoctorId",
                principalTable: "TbDoctors",
                principalColumn: "Id");
        }
    }
}
