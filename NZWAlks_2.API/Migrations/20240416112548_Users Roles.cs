using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWAlks_2.API.Migrations
{
    /// <inheritdoc />
    public partial class UsersRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_Roles_RoleId",
                table: "User_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_Users_UserId",
                table: "User_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Roles",
                table: "User_Roles");

            migrationBuilder.RenameTable(
                name: "User_Roles",
                newName: "Users_Roles");

            migrationBuilder.RenameIndex(
                name: "IX_User_Roles_UserId",
                table: "Users_Roles",
                newName: "IX_Users_Roles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Roles_RoleId",
                table: "Users_Roles",
                newName: "IX_Users_Roles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Roles",
                table: "Users_Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Roles_RoleId",
                table: "Users_Roles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Users_UserId",
                table: "Users_Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Roles_RoleId",
                table: "Users_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Users_UserId",
                table: "Users_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Roles",
                table: "Users_Roles");

            migrationBuilder.RenameTable(
                name: "Users_Roles",
                newName: "User_Roles");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Roles_UserId",
                table: "User_Roles",
                newName: "IX_User_Roles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Roles_RoleId",
                table: "User_Roles",
                newName: "IX_User_Roles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Roles",
                table: "User_Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_Roles_RoleId",
                table: "User_Roles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_Users_UserId",
                table: "User_Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
