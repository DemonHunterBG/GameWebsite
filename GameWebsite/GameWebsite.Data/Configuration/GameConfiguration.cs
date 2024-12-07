using GameWebsite.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasData(this.SeedGames());
        }

        private List<Game> SeedGames()
        {
            List<Game> games = new List<Game>()
            {
                new Game
                {
                    Id = 1,
                    Name = "DemonicaS",
                    GameURL = "http://127.0.0.1/DemonicaS/",
                    ImageURL = "http://127.0.0.1/DemonicaS/Icon.png",
                    Description = "You are a demon. The last of your kind. Cursed to fight to the bloody end again and again. Humanity on one side and abominations on the other.\r\n\r\nFIGHT ON!!!\r\n\r\n\r\n\r\n\r\nMade for GameDev.tv Game Jam 2024\r\n\r\nTheme: Last Stand\r\n\r\n\r\n\r\nLeave a comment with the wave you reached!\r\n\r\nIf you like this game, consider checking out my other ones!"
                },
                new Game
                {
                    Id = 2,
                    Name = "Interstellar Submarine",
                    GameURL = "http://127.0.0.1/Interstellar Submarine 2.1.0 Webgl/",
                    ImageURL = "http://127.0.0.1/Interstellar Submarine 2.1.0 Webgl/Icon.png",
                    Description = "Travel across a dozen different locations to defeat a multitude of different enemy spacecraft while avoiding their weaponry and upgrading your own ship in this top down shooter!\r\n\r\nThis is my first larger game. I hope you enjoy it!\r\n\r\nSpecial thanks to CORPVS for the music in the game."
                },
                new Game
                {
                    Id = 3,
                    Name = "Loop Miner",
                    GameURL = "http://127.0.0.1/Loop Miner 1.4/",
                    ImageURL = "http://127.0.0.1/Loop Miner 1.4/Icon.png",
                    Description = "Top down mining game.\r\n\r\n Made for Mini Jame Gam #21\r\n\r\nControls:\r\n\r\nMovement - WASD\r\nMining - Run into blocks\r\nFINAL UPDATE 1.4\r\n\r\n4 New Biomes\r\n10 New Blocks\r\n3 New Upgrades\r\nUI Changes\r\nMusic - Olexy on pixabay"
                },
                new Game
                {
                    Id = 4,
                    Name = "Oil Tycoon",
                    GameURL = "http://127.0.0.1/Oil Tycoon Webgl/",
                    ImageURL = "http://127.0.0.1/Oil Tycoon Webgl/Icon.png",
                    Description = "Made for Fireside Jam 2024\r\n\r\nBuild and expand your oil business to 3 different locations!\r\n\r\nIncludes:\r\n\r\n3 Different locations\r\n13 Structures to build\r\n16 Challenges to complete\r\nMusic - public domain verion of the 1718-1720 'Winter' by Antonio Vivaldi"
                },
                new Game
                {
                    Id = 5,
                    Name = "Spring Madness",
                    GameURL = "http://127.0.0.1/Spring Madness Build/",
                    ImageURL = "http://127.0.0.1/Spring Madness Build/Icon.png",
                    Description = "Controls:\r\n\r\n-Click on flowers to collect them with left mouse button\r\n\r\n-Click on upgade buttons with left mouse button\r\n\r\nCredits:\r\n\r\nMusic - Devonshire Waltz Allegretto Kevin MacLeod (incompetech.com)"
                },
            };

            return games;
        }
    }
}
