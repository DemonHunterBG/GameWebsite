using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChristmasTree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddedOn", "ArtworkURL", "Title" },
                values: new object[] { 6, new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Local), "/images/artworks/Christmas Tree.png", "Christmas Tree" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
