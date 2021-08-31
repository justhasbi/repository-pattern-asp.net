using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class initial_migrations_add_relations_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Profiling_Educations",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_University_Education_Universities",
                table: "University");

            migrationBuilder.RenameColumn(
                name: "Universities",
                table: "University",
                newName: "EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_University_Universities",
                table: "University",
                newName: "IX_University_EducationId");

            migrationBuilder.RenameColumn(
                name: "Educations",
                table: "Education",
                newName: "ProfilingNIK");

            migrationBuilder.RenameIndex(
                name: "IX_Education_Educations",
                table: "Education",
                newName: "IX_Education_ProfilingNIK");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Profiling_ProfilingNIK",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_University_Education_EducationId",
                table: "University");

            migrationBuilder.RenameColumn(
                name: "EducationId",
                table: "University",
                newName: "Universities");

            migrationBuilder.RenameIndex(
                name: "IX_University_EducationId",
                table: "University",
                newName: "IX_University_Universities");

            migrationBuilder.RenameColumn(
                name: "ProfilingNIK",
                table: "Education",
                newName: "Educations");

            migrationBuilder.RenameIndex(
                name: "IX_Education_ProfilingNIK",
                table: "Education",
                newName: "IX_Education_Educations");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Profiling_Educations",
                table: "Education",
                column: "Educations",
                principalTable: "Profiling",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_University_Education_Universities",
                table: "University",
                column: "Universities",
                principalTable: "Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
