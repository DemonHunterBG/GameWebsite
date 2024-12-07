using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedOn",
                value: new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedOn",
                value: new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddedOn",
                value: new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddedOn",
                value: new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddedOn",
                value: new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddedOn",
                value: new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AddedOn", "Description", "GameURL", "ImageURL", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), "You are a demon. The last of your kind. Cursed to fight to the bloody end again and again. Humanity on one side and abominations on the other.\r\n\r\nFIGHT ON!!!\r\n\r\n\r\n\r\n\r\nMade for GameDev.tv Game Jam 2024\r\n\r\nTheme: Last Stand\r\n\r\n\r\n\r\nLeave a comment with the wave you reached!\r\n\r\nIf you like this game, consider checking out my other ones!", "http://127.0.0.1/DemonicaS/", "http://127.0.0.1/DemonicaS/Icon.png", "DemonicaS" },
                    { 2, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), "Travel across a dozen different locations to defeat a multitude of different enemy spacecraft while avoiding their weaponry and upgrading your own ship in this top down shooter!\r\n\r\nThis is my first larger game. I hope you enjoy it!\r\n\r\nSpecial thanks to CORPVS for the music in the game.", "http://127.0.0.1/Interstellar Submarine 2.1.0 Webgl/", "http://127.0.0.1/Interstellar Submarine 2.1.0 Webgl/Icon.png", "Interstellar Submarine" },
                    { 3, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), "Top down mining game.\r\n\r\n Made for Mini Jame Gam #21\r\n\r\nControls:\r\n\r\nMovement - WASD\r\nMining - Run into blocks\r\nFINAL UPDATE 1.4\r\n\r\n4 New Biomes\r\n10 New Blocks\r\n3 New Upgrades\r\nUI Changes\r\nMusic - Olexy on pixabay", "http://127.0.0.1/Loop Miner 1.4/", "http://127.0.0.1/Loop Miner 1.4/Icon.png", "Loop Miner" },
                    { 4, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), "Made for Fireside Jam 2024\r\n\r\nBuild and expand your oil business to 3 different locations!\r\n\r\nIncludes:\r\n\r\n3 Different locations\r\n13 Structures to build\r\n16 Challenges to complete\r\nMusic - public domain verion of the 1718-1720 'Winter' by Antonio Vivaldi", "http://127.0.0.1/Oil Tycoon Webgl/", "http://127.0.0.1/Oil Tycoon Webgl/Icon.png", "Oil Tycoon" },
                    { 5, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), "Controls:\r\n\r\n-Click on flowers to collect them with left mouse button\r\n\r\n-Click on upgade buttons with left mouse button\r\n\r\nCredits:\r\n\r\nMusic - Devonshire Waltz Allegretto Kevin MacLeod (incompetech.com)", "http://127.0.0.1/Spring Madness Build/", "http://127.0.0.1/Spring Madness Build/Icon.png", "Spring Madness" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName" },
                values: new object[,]
                {
                    { 1, "Adventure" },
                    { 2, "Action" },
                    { 3, "Sports" },
                    { 4, "Simulation" },
                    { 5, "Platformer" },
                    { 6, "RPG" },
                    { 7, "First-person Shooter" },
                    { 8, "Action-adventure" },
                    { 9, "Strategy" },
                    { 10, "Real-time Strategy" },
                    { 11, "Strategy" },
                    { 12, "Grand Strategy" },
                    { 13, "Casual" },
                    { 14, "Fighting" },
                    { 15, "MMO" },
                    { 16, "Stealth" },
                    { 17, "Survival" },
                    { 18, "Racing" },
                    { 19, "Horror" },
                    { 20, "Puzzle" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedOn",
                value: new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedOn",
                value: new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddedOn",
                value: new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddedOn",
                value: new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddedOn",
                value: new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddedOn",
                value: new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
