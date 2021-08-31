using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class initial_migrations_add_relations_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Profiling_ProfilingNIK",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_University_Education_EducationId",
                table: "University");

            migrationBuilder.DropIndex(
                name: "IX_University_EducationId",
                table: "University");

            migrationBuilder.DropIndex(
                name: "IX_Education_ProfilingNIK",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "University");

            migrationBuilder.DropColumn(
                name: "ProfilingNIK",
                table: "Education");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Education",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Profiling_EducationId",
                table: "Profiling",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_UniversityId",
                table: "Education",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling");

            migrationBuilder.DropIndex(
                name: "IX_Profiling_EducationId",
                table: "Profiling");

            migrationBuilder.DropIndex(
                name: "IX_Education_UniversityId",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Profiling");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Education");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "University",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilingNIK",
                table: "Education",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_University_EducationId",
                table: "University",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_ProfilingNIK",
                table: "Education",
                column: "ProfilingNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Profiling_ProfilingNIK",
                table: "Education",
                column: "ProfilingNIK",
                principalTable: "Profiling",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_University_Education_EducationId",
                table: "University",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
