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
    public class ArtworkConfiguration : IEntityTypeConfiguration<Artwork>
    {
        public void Configure(EntityTypeBuilder<Artwork> builder) 
        {
            builder.HasData(this.SeedArtworks());
        }

        private List<Artwork> SeedArtworks()
        {
            List<Artwork> artworks = new List<Artwork>()
            {
                new Artwork
                {
                    Id = 1,
                    Title = "Forest",
                    ArtworkURL = "/images/artworks/Forest.png"
                },
                new Artwork
                {
                    Id = 2,
                    Title = "Lizard Monster",
                    ArtworkURL = "/images/artworks/Lizard Monster.png"
                },
                new Artwork
                {
                    Id = 3,
                    Title = "Snel",
                    ArtworkURL = "/images/artworks/Snel.png"
                },
                new Artwork
                {
                    Id = 4,
                    Title = "Wolf",
                    ArtworkURL = "/images/artworks/Wolf.png"
                },
                new Artwork
                {
                    Id = 5,
                    Title = "Zoom Rocket",
                    ArtworkURL = "/images/artworks/Zoom Rocket.png"
                },
                new Artwork
                {
                    Id = 6,
                    Title = "Christmas Tree",
                    ArtworkURL = "/images/artworks/Christmas Tree.png"
                }
            };

            return artworks;
        }
    }
}
