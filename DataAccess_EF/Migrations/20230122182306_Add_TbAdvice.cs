using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Add_TbAdvice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "TbAdvices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiseaseTypeId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAdvices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbAdvices_TbDiseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "TbDiseases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbAdvices_TbDiseaseTypes_DiseaseTypeId",
                        column: x => x.DiseaseTypeId,
                        principalTable: "TbDiseaseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbAdvices_TbDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "TbDoctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbAdvices_DiseaseId",
                table: "TbAdvices",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TbAdvices_DiseaseTypeId",
                table: "TbAdvices",
                column: "DiseaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbAdvices_DoctorId",
                table: "TbAdvices",
                column: "DoctorId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TbAdvices");

            migrationBuilder.DropTable(
                name: "TbClinicImages");

            migrationBuilder.DropTable(
                name: "TbContacts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TbDiseases");

            migrationBuilder.DropTable(
                name: "TbDoctors");

            migrationBuilder.DropTable(
                name: "TbDiseaseTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TbSpecialization");
        }
    }
}
