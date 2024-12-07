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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(this.SeedGenres());
        }

        private List<Genre> SeedGenres()
        {
            List<Genre> genres = new List<Genre>()
            {
                new Genre
                {
                    Id = 1,
                    GenreName = "Adventure",
                },
                new Genre
                {
                    Id = 2,
                    GenreName = "Action",
                },
                new Genre
                {
                    Id = 3,
                    GenreName = "Sports",
                },
                new Genre
                {
                    Id = 4,
                    GenreName = "Simulation",
                },
                new Genre
                {
                    Id = 5,
                    GenreName = "Platformer",
                },
                new Genre
                {
                    Id = 6,
                    GenreName = "RPG",
                },new Genre
                {
                    Id = 7,
                    GenreName = "First-person Shooter",
                },
                new Genre
                {
                    Id = 8,
                    GenreName = "Action-adventure",
                },
                new Genre
                {
                    Id = 9,
                    GenreName = "Strategy",
                },
                new Genre
                {
                    Id = 10,
                    GenreName = "Real-time Strategy",
                },
                new Genre
                {
                    Id = 11,
                    GenreName = "Grand Strategy",
                },
                new Genre
                {
                    Id = 12,
                    GenreName = "Rougelite",
                },
                new Genre
                {
                    Id = 13,
                    GenreName = "Casual",
                },
                new Genre
                {
                    Id = 14,
                    GenreName = "Fighting",
                },
                new Genre
                {
                    Id = 15,
                    GenreName = "MMO",
                },
                new Genre
                {
                    Id = 16,
                    GenreName = "Stealth",
                },
                new Genre
                {
                    Id = 17,
                    GenreName = "Survival",
                },
                new Genre
                {
                    Id = 18,
                    GenreName = "Racing",
                },
                new Genre
                {
                    Id = 19,
                    GenreName = "Horror",
                },
                new Genre
                {
                    Id = 20,
                    GenreName = "Puzzle",
                },
            };

            return genres;
        }
    }
}
