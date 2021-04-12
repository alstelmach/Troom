using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Infrastructure.Persistence.Read.Migrations
{
    public partial class LinkedUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleDtoUserDto",
                schema: "UserRead",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDtoUserDto", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleDtoUserDto_Roles_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "UserRead",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleDtoUserDto_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "UserRead",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleDtoUserDto_UsersId",
                schema: "UserRead",
                table: "RoleDtoUserDto",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleDtoUserDto",
                schema: "UserRead");
        }
    }
}
