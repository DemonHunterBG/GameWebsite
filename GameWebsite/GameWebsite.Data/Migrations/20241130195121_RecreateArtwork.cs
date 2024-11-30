using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecreateArtwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ArtworkURL = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.Id);
                });

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
            migrationBuilder.DropTable(
                name: "Artworks");

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

