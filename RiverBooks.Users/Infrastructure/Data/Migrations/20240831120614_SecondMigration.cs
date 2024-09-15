using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiverBooks.Users.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStreetAdress",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StreetAddress_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StreetAddress_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StreetAddress_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StreetAddress_Street1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StreetAddress_Street2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StreetAddress_ZipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStreetAdress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStreetAdress_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Users",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStreetAdress_ApplicationUserId",
                schema: "Users",
                table: "UserStreetAdress",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStreetAdress",
                schema: "Users");
        }
    }
}
