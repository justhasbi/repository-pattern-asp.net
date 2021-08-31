using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class latest_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Persons_NIK",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Account_NIK",
                table: "Profiling");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_University",
                table: "University");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiling",
                table: "Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "University",
                newName: "Universities");

            migrationBuilder.RenameTable(
                name: "Profiling",
                newName: "Profilings");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Profiling_EducationId",
                table: "Profilings",
                newName: "IX_Profilings_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_UniversityId",
                table: "Educations",
                newName: "IX_Educations_UniversityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Universities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GPA",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Universities",
                table: "Universities",
                column: "UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profilings",
                table: "Profilings",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "EducationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Persons_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "Persons",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Accounts_NIK",
                table: "Profilings",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Persons_NIK",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Accounts_NIK",
                table: "Profilings");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Universities",
                table: "Universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profilings",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "University");

            migrationBuilder.RenameTable(
                name: "Profilings",
                newName: "Profiling");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Profilings_EducationId",
                table: "Profiling",
                newName: "IX_Profiling_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UniversityId",
                table: "Education",
                newName: "IX_Education_UniversityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "University",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GPA",
                table: "Education",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "Education",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_University",
                table: "University",
                column: "UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiling",
                table: "Profiling",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "EducationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Persons_NIK",
                table: "Account",
                column: "NIK",
                principalTable: "Persons",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Account_NIK",
                table: "Profiling",
                column: "NIK",
                principalTable: "Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
