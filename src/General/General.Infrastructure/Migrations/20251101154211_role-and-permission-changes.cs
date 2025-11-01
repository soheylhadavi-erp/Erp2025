using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace General.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class roleandpermissionchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_SystemPermissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_SystemPermissions_PermissionId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "UserPermissions",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserPermissions",
                newName: "PermissionsId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UsersId");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "RolePermissions",
                newName: "RolesId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RolePermissions",
                newName: "PermissionsId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RolesId",
                table: "RolePermissions",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_SystemPermissions_PermissionsId",
                table: "RolePermissions",
                column: "PermissionsId",
                principalTable: "SystemPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_SystemPermissions_PermissionsId",
                table: "UserPermissions",
                column: "PermissionsId",
                principalTable: "SystemPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_UsersId",
                table: "UserPermissions",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RolesId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_SystemPermissions_PermissionsId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_SystemPermissions_PermissionsId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_UsersId",
                table: "UserPermissions");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "UserPermissions",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "PermissionsId",
                table: "UserPermissions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UsersId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_PermissionId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RolePermissions",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "PermissionsId",
                table: "RolePermissions",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RolesId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_SystemPermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "SystemPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_SystemPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId",
                principalTable: "SystemPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
