using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess_EF.Migrations
{
    public partial class Add_TbLungCancer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbLungCancer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AirPollution = table.Column<int>(type: "int", nullable: false),
                    Alcoholuse = table.Column<int>(type: "int", nullable: false),
                    DustAllergy = table.Column<int>(type: "int", nullable: false),
                    OccuPationalHazards = table.Column<int>(type: "int", nullable: false),
                    GeneticRisk = table.Column<int>(type: "int", nullable: false),
                    ChronicLungDisease = table.Column<int>(type: "int", nullable: false),
                    BalancedDiet = table.Column<int>(type: "int", nullable: false),
                    Obesity = table.Column<int>(type: "int", nullable: false),
                    Smoking = table.Column<int>(type: "int", nullable: false),
                    PassiveSmoker = table.Column<int>(type: "int", nullable: false),
                    ChestPain = table.Column<int>(type: "int", nullable: false),
                    CoughingofBlood = table.Column<int>(type: "int", nullable: false),
                    Fatigue = table.Column<int>(type: "int", nullable: false),
                    WeightLoss = table.Column<int>(type: "int", nullable: false),
                    ShortnessofBreath = table.Column<int>(type: "int", nullable: false),
                    Wheezing = table.Column<int>(type: "int", nullable: false),
                    SwallowingDifficulty = table.Column<int>(type: "int", nullable: false),
                    ClubbingofFingerNail = table.Column<int>(type: "int", nullable: false),
                    FrequentCold = table.Column<int>(type: "int", nullable: false),
                    DryCough = table.Column<int>(type: "int", nullable: false),
                    Snoring = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbLungCancer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbLungCancer_TbDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "TbDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbLungCancer_DoctorId",
                table: "TbLungCancer",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbLungCancer");
        }
    }
}
