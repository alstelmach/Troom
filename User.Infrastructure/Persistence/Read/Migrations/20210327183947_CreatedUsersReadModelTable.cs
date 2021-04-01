using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Infrastructure.Persistence.Read.Migrations
{
    public partial class CreatedUsersReadModelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UserRead");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "UserRead",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(1000)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(1000)", nullable: false),
                    MailAddress = table.Column<string>(type: "varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "UserRead");
        }
    }
}
