using GameWebsite.Data.Models;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.ViewModels.AdminViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Services.Data
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre, int> genreRepository;
        private readonly IRepository<GameGenre, object> gameGenreRepository;

        public GenreService(IRepository<Genre, int> genreRepository, IRepository<GameGenre, object> gameGenreRepository)
        {
            this.genreRepository = genreRepository;
            this.gameGenreRepository = gameGenreRepository;
        }

        public async Task<IEnumerable<GenreViewModel>> GetAllAsync()
        {
            var genreViewModels = await this.genreRepository
                .GetAllAttached()
                .Select(g => new GenreViewModel()
                {
                    Id = g.Id,
                    GenreName = g.GenreName,
                })
                .AsNoTracking()
                .ToListAsync();

            return genreViewModels;
        }

        public async Task AddAsync(AddGenreViewModel model)
        {
            Genre genre = new Genre()
            {
                GenreName = model.GenreName
            };

            this.genreRepository.AddAsync(genre);
        }

        public async Task<AddGenreViewModel> GetByIdAttachedAsync(int id)
        {
            var model = await genreRepository
                .GetAllAttached()
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(g => new AddGenreViewModel
                {
                    GenreName = g.GenreName,
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            var model = await genreRepository
                .GetByIDAsync(id);

            return model;
        }

        public async Task UpdateAsync(Genre entity, AddGenreViewModel model)
        {
            entity.GenreName = model.GenreName;

            bool result = await genreRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entities = await gameGenreRepository
                .GetAllAttached()
                .Where(gg => gg.GenreId == id)
                .ToListAsync();

            foreach (var entity in entities)
            {
                bool gameGenreResult = await gameGenreRepository.DeleteEntityAsync(entity);
            }

            bool result = await genreRepository.DeleteAsync(id);
        }
    }
}
