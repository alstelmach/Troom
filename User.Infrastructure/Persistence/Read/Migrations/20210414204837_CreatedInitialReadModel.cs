using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Infrastructure.Persistence.Read.Migrations
{
    public partial class CreatedInitialReadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "userreadmodel");

            migrationBuilder.CreateTable(
                name: "PermissionRoleAssignments",
                schema: "userreadmodel",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRoleAssignments", x => new { x.RoleId, x.PermissionId });
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "userreadmodel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "userreadmodel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: true),
                    MailAddress = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleAssignments",
                schema: "userreadmodel",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleAssignments", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserRoleAssignments_Roles_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "userreadmodel",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleAssignments_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "userreadmodel",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAssignments_UsersId",
                schema: "userreadmodel",
                table: "UserRoleAssignments",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionRoleAssignments",
                schema: "userreadmodel");

            migrationBuilder.DropTable(
                name: "UserRoleAssignments",
                schema: "userreadmodel");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "userreadmodel");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "userreadmodel");
        }
    }
}
