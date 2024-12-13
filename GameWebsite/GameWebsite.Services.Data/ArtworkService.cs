using GameWebsite.Data.Models;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.ViewModels.AdminViewModels;
using GameWebsite.Web.ViewModels.Artwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Services.Data
{
    public class ArtworkService : IArtworkService
    {
        private readonly IRepository<Artwork, int> artworkRepository;

        public ArtworkService(IRepository<Artwork, int> artworkRepository)
        {
            this.artworkRepository = artworkRepository;
        }

        public async Task<IEnumerable<Artwork>> GetAllAsync()
        {
            var artworks = await this.artworkRepository
                .GetAllAttached()
                .AsNoTracking()
                .ToListAsync();

            return artworks;
        }

        public async Task<Artwork> GetByIdAsync(int id)
        {
            var model = await artworkRepository
                .GetByIDAsync(id);

            return model;
        }

        public async Task AddAsync(AddArtworkInputModel model)
        {
            Artwork genre = new Artwork()
            {
                Title = model.Title,
                ArtworkURL = model.ArtworkURL,
            };

            await this.artworkRepository.AddAsync(genre);
        }

        public async Task DeleteAsync(int id)
        {
            bool result = await artworkRepository.DeleteAsync(id);
        }
    }
}
