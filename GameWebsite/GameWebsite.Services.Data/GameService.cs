using GameWebsite.Data.Models;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.ViewModels.AdminViewModels;
using GameWebsite.Web.ViewModels.Game;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Services.Data
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game, int> gameRepository;
        private readonly IRepository<Genre, int> genreRepository;
        private readonly IRepository<GameGenre, object> gameGenreRepository;
        private readonly IRepository<ApplicationUserGame, object> applicationUserGamesRepository;
        private readonly IRepository<GameComment, int> gameCommentsRepository;

        public GameService(
            IRepository<Game, int> gameRepository, 
            IRepository<Genre, int> genreRepository, 
            IRepository<GameGenre, object> gameGenreRepository, 
            IRepository<ApplicationUserGame, object> applicationUserGamesRepository, 
            IRepository<GameComment, int> gameCommentsRepository)
        {
            this.gameRepository = gameRepository;
            this.genreRepository = genreRepository;
            this.gameGenreRepository = gameGenreRepository;
            this.applicationUserGamesRepository = applicationUserGamesRepository;
            this.gameCommentsRepository = gameCommentsRepository;
        }

        public async Task<bool> CheckIfGameExists(int id)
        {
            bool exists = await gameRepository
                .GetAllAttached()
                .AnyAsync(g => g.Id == id);

            return exists;
        }

        public async Task<IEnumerable<GameListViewModel>> GetAllWithQueryAsync(string? currentUserId = null, string? searchQuery = null, string? genre = null)
        {
            var games = await gameRepository
                .GetAllAttached()
                .AsNoTracking()
                .Include(g => g.Favorites)
                .Include(g => g.Genres)
                .ThenInclude(g => g.Genre)
                .ToListAsync();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();
                games = games.Where(g => g.Name.ToLower().Contains(searchQuery)).ToList();
            }

            if (!string.IsNullOrEmpty(genre))
            {
                genre = genre.ToLower().Trim();
                games = games.Where(g => g.Genres.Any(ge => ge.Genre.GenreName.ToLower().Contains(genre))).ToList();
            }

            var gamesUpdated = games.Select(g => new GameListViewModel()
            {
                Id = g.Id,
                Name = g.Name,
                ImageURL = g.ImageURL,
                HasFavored = g.Favorites.Any(f => f.UserId == currentUserId),
            }).ToList();

            return gamesUpdated;
        }

        public async Task<IEnumerable<GameListViewModel>> GetAllFavoritesAsync(string currentUserId)
        {
            var games = await gameRepository
                .GetAllAttached()
                .Where(g => g.Favorites.Any(f => f.UserId == currentUserId))
                .Select(g => new GameListViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageURL = g.ImageURL,
                    HasFavored = true,
                })
                .AsNoTracking()
                .ToListAsync();

            return games;
        }

        public async Task<IEnumerable<GameManagementViewModel>> GetAllManagementAsync()
        {
            var allGenres = await genreRepository.GetAllAsync();

            var gameViewModels = await gameRepository
                .GetAllAttached()
                .Select(g => new GameManagementViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Genres = g.Genres.Select(g => g.Genre).ToList(),
                    AllGenres = allGenres.ToList(),
                })
                .AsNoTracking()
                .ToListAsync();

            return gameViewModels;
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            var model = await gameRepository
                .GetByIDAsync(id);

            return model;
        }

        public async Task<GameViewModel> GetGameForPageByIdAsync(int id, string currentUserId)
        {
            var model = await gameRepository
                .GetAllAttached()
                .Where(g => g.Id == id)
                .AsNoTracking()
                .Select(g => new GameViewModel()
                {
                    Id = id,
                    Name = g.Name,
                    GameURL = g.GameURL,
                    IsGameURLWorking = false,
                    Description = g.Description,
                    AddedOn = g.AddedOn,
                    Genres = g.Genres.Select(g => g.Genre.GenreName).ToList(),
                    Comments = g.Comments.Select(c => new GameCommentViewModel()
                    {
                        Id = c.Id,
                        Text = c.Text,
                        AddedOn = c.AddedOn,
                        CreatorName = c.User.UserName,
                        IsCreator = c.UserId == currentUserId ? true : false,
                    }).ToList(),
                })
                .FirstOrDefaultAsync();

            Uri uriResult;
            bool result = Uri.TryCreate(model.GameURL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (uriResult != null && uriResult.IsAbsoluteUri)
            {
                HttpWebResponse response = null;
                var request = (HttpWebRequest)WebRequest.Create(model.GameURL + "/index.html");
                request.Method = "HEAD";

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    /* A WebException will be thrown if the status of the response is not `200 OK` */
                }
                finally
                {
                    if (response != null)
                    {
                        model.IsGameURLWorking = true;
                        response.Close();
                    }
                }
            }

            return model;
        }

        public async Task<AddGameViewModel> GetByIdForEditAsync(int id)
        {
            var model = await gameRepository
                .GetAllAttached()
                .Where(g => g.Id == id)
                .Select(g => new AddGameViewModel
                {
                    Name = g.Name,
                    GameURL = g.GameURL,
                    ImageURL = g.ImageURL,
                    Description = g.Description,
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task AddAsync(AddGameViewModel model)
        {
            Game game = new Game()
            {
                Name = model.Name,
                GameURL = model.GameURL,
                ImageURL = model.ImageURL,
                Description = model.Description,
            };

            await this.gameRepository.AddAsync(game);
        }

        public async Task AssignGenre(int gameId, int genreId)
        {
            if (!await gameGenreRepository.GetAllAttached().AnyAsync(gg => gg.GameId == gameId && gg.GenreId == genreId))
            {
                GameGenre gameGenre = new GameGenre()
                {
                    GameId = gameId,
                    GenreId = genreId,
                };

                await gameGenreRepository.AddAsync(gameGenre);
            }
        }

        public async Task RemoveGenre(int gameId, int genreId)
        {
            if (await gameGenreRepository.GetAllAttached().AnyAsync(gg => gg.GameId == gameId && gg.GenreId == genreId))
            {
                var entity = await gameGenreRepository
                    .GetAllAttached()
                    .Where(gg => gg.GameId == gameId && gg.GenreId == genreId)
                    .FirstAsync();

                await gameGenreRepository.DeleteEntityAsync(entity);
            }
        }

        public async Task AddToFavoritesAsync(string currentUserId, int gameId)
        {
            if (!await applicationUserGamesRepository.GetAllAttached().AnyAsync(f => f.UserId == currentUserId && f.GameId == gameId))
            {
                ApplicationUserGame applicationUserGame = new ApplicationUserGame()
                {
                    UserId = currentUserId,
                    GameId = gameId
                };

                await applicationUserGamesRepository.AddAsync(applicationUserGame);
            }
        }

        public async Task RemoveFromFavoritesAsync(string currentUserId, int gameId)
        {
            ApplicationUserGame? current = await applicationUserGamesRepository.GetAllAttached().FirstOrDefaultAsync(f => f.UserId == currentUserId && f.GameId == gameId);

            if (current != null)
            {
                await applicationUserGamesRepository.DeleteEntityAsync(current);
            }
        }

        public async Task UpdateAsync(Game entity, AddGameViewModel model)
        {
            entity.Name = model.Name;
            entity.GameURL = model.GameURL;
            entity.ImageURL = model.ImageURL;
            entity.Description = model.Description;

            bool result = await gameRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var gameGenres = await gameGenreRepository
                .GetAllAttached()
                .Where(gg => gg.GameId == id)
                .ToListAsync();

            foreach (var gameGenre in gameGenres)
            {
                bool gameGenreResult = await gameGenreRepository.DeleteEntityAsync(gameGenre);
            }

            var applicationUserGames = await applicationUserGamesRepository
                .GetAllAttached()
                .Where(gg => gg.GameId == id)
                .ToListAsync();

            foreach (var applicationUserGame in applicationUserGames)
            {
                bool applicationUserGameResult = await applicationUserGamesRepository.DeleteEntityAsync(applicationUserGame);
            }

            var gameComments = await gameCommentsRepository
                .GetAllAttached()
                .Where(gg => gg.GameId == id)
                .ToListAsync();

            foreach (var gameComment in gameComments)
            {
                bool gameCommentResult = await gameCommentsRepository.DeleteEntityAsync(gameComment);
            }


            bool result = await gameRepository.DeleteAsync(id);
        }
    }
}
