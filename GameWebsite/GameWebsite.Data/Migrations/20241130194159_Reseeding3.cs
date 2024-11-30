using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class Reseeding3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Lizard Monster");

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Snel");

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "Christmas Tree");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Lizard Monster 2");

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Snel 2");

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "Christmas Tree 2");
        }
    }
}
