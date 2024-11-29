using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArtworkSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddedOn", "ArtworkURL", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Local), "/images/artworks/Forest.png", "Forest" },
                    { 2, new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Local), "/images/artworks/Lizard Monster.png", "Lizard Monster" },
                    { 3, new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Local), "/images/artworks/Snel.png", "Snel" },
                    { 4, new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Local), "/images/artworks/Wolf.png", "Wolf" },
                    { 5, new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Local), "/images/artworks/Zoom Rocket.png", "Zoom Rocket" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
