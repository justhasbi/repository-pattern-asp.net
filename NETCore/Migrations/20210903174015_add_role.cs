using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class add_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "tb_m_university");

            migrationBuilder.RenameTable(
                name: "Profilings",
                newName: "tb_tr_profiling");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "tb_m_person");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "tb_m_education");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "tb_m_account");

            migrationBuilder.RenameIndex(
                name: "IX_Profilings_EducationId",
                table: "tb_tr_profiling",
                newName: "IX_tb_tr_profiling_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UniversityId",
                table: "tb_m_education",
                newName: "IX_tb_m_education_UniversityId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "tb_m_person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_university",
                table: "tb_m_university",
                column: "UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_profiling",
                table: "tb_tr_profiling",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_person",
                table: "tb_m_person",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_education",
                table: "tb_m_education",
                column: "EducationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_account",
                table: "tb_m_account",
                column: "NIK");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => x.AccountRoleId);
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_tb_m_account_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "tb_m_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_AccountNIK",
                table: "AccountRole",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RoleId",
                table: "AccountRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_tb_m_person_NIK",
                table: "tb_m_account",
                column: "NIK",
                principalTable: "tb_m_person",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_education_tb_m_university_UniversityId",
                table: "tb_m_education",
                column: "UniversityId",
                principalTable: "tb_m_university",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_profiling_tb_m_account_NIK",
                table: "tb_tr_profiling",
                column: "NIK",
                principalTable: "tb_m_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_profiling_tb_m_education_EducationId",
                table: "tb_tr_profiling",
                column: "EducationId",
                principalTable: "tb_m_education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_tb_m_person_NIK",
                table: "tb_m_account");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_education_tb_m_university_UniversityId",
                table: "tb_m_education");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_profiling_tb_m_account_NIK",
                table: "tb_tr_profiling");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_profiling_tb_m_education_EducationId",
                table: "tb_tr_profiling");

            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_profiling",
                table: "tb_tr_profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_university",
                table: "tb_m_university");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_person",
                table: "tb_m_person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_education",
                table: "tb_m_education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_account",
                table: "tb_m_account");

            migrationBuilder.RenameTable(
                name: "tb_tr_profiling",
                newName: "Profilings");

            migrationBuilder.RenameTable(
                name: "tb_m_university",
                newName: "Universities");

            migrationBuilder.RenameTable(
                name: "tb_m_person",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "tb_m_education",
                newName: "Educations");

            migrationBuilder.RenameTable(
                name: "tb_m_account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_profiling_EducationId",
                table: "Profilings",
                newName: "IX_Profilings_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_education_UniversityId",
                table: "Educations",
                newName: "IX_Educations_UniversityId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profilings",
                table: "Profilings",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Universities",
                table: "Universities",
                column: "UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
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
    }
}
