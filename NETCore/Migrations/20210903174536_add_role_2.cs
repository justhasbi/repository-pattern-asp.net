using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class add_role_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_tb_m_account_AccountNIK",
                table: "AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "tb_m_role");

            migrationBuilder.RenameTable(
                name: "AccountRole",
                newName: "tb_tr_account_role");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRole_RoleId",
                table: "tb_tr_account_role",
                newName: "IX_tb_tr_account_role_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRole_AccountNIK",
                table: "tb_tr_account_role",
                newName: "IX_tb_tr_account_role_AccountNIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_role",
                table: "tb_m_role",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_account_role",
                table: "tb_tr_account_role",
                column: "AccountRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_role_tb_m_account_AccountNIK",
                table: "tb_tr_account_role",
                column: "AccountNIK",
                principalTable: "tb_m_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_role_tb_m_role_RoleId",
                table: "tb_tr_account_role",
                column: "RoleId",
                principalTable: "tb_m_role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_role_tb_m_account_AccountNIK",
                table: "tb_tr_account_role");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_role_tb_m_role_RoleId",
                table: "tb_tr_account_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_account_role",
                table: "tb_tr_account_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_role",
                table: "tb_m_role");

            migrationBuilder.RenameTable(
                name: "tb_tr_account_role",
                newName: "AccountRole");

            migrationBuilder.RenameTable(
                name: "tb_m_role",
                newName: "Role");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_account_role_RoleId",
                table: "AccountRole",
                newName: "IX_AccountRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_account_role_AccountNIK",
                table: "AccountRole",
                newName: "IX_AccountRole_AccountNIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole",
                column: "AccountRoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_tb_m_account_AccountNIK",
                table: "AccountRole",
                column: "AccountNIK",
                principalTable: "tb_m_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
