using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RiverBooks.Books.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "books");

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "books",
                table: "Books",
                columns: new[] { "Id", "Author", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("b1a82ab8-76e4-4bb4-8b4f-7a1b9ef3340f"), "J.R.R. Tolkien", 9.99m, "The Two Towers" },
                    { new Guid("c2a29c12-5c6f-4f13-a0d2-f0930c5e63e4"), "J.R.R. Tolkien", 9.99m, "The Return of the King" },
                    { new Guid("e3f0c672-4c8a-4d88-a379-9c55d7bb91a1"), "J.R.R. Tolkien", 9.99m, "The Fellowship of the Ring" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books",
                schema: "books");
        }
    }
}
