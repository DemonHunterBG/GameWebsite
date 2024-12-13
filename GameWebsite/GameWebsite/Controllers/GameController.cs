using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Web.ViewModels.Artwork;
using GameWebsite.Web.ViewModels.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GameWebsite.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext context;
        private IRepository<Game, int> gameRepository;

        public GameController(ApplicationDbContext context, IRepository<Game, int> gameRepository)
        {
            this.context = context;
            this.gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchQuery = null, string? genre = null)
        {

            var games = await context.Games
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
                HasFavored = g.Favorites.Any(f => f.UserId == GetCurrentUserId()),
            }).ToList();

            ViewData["SearchQuery"] = searchQuery;
            ViewData["Genre"] = genre;

            return View(gamesUpdated);
        }

        [HttpGet]
        public async Task<IActionResult> Game(int id)
        {
            if (!context.Games.Any(g => g.Id == id))
            {
                return RedirectToAction(nameof(Index));
            }

            var model = await context.Games
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
                        IsCreator = c.UserId == GetCurrentUserId() ? true : false,
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

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var games = await context.Games
                .Where(g => g.Favorites.Any(f => f.UserId == GetCurrentUserId()))
                .Select(g => new GameListViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageURL = g.ImageURL,
                    HasFavored = true,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(games);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int gameId)
        {
            Game? entity = await context.Games
                .Where(g => g.Id == gameId)
                .Include(g => g.Favorites)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                return RedirectToAction(nameof(Index));
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!entity.Favorites.Any(f => f.UserId == currentUserId))
            {
                entity.Favorites.Add(new ApplicationUserGame()
                {
                    UserId = currentUserId,
                    GameId = gameId
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Favorites));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int gameId)
        {
            Game? entity = await context.Games
            .Where(g => g.Id == gameId)
            .Include(g => g.Favorites)
            .FirstOrDefaultAsync();

            if (entity == null)
            {
                return RedirectToAction(nameof(Favorites));
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            ApplicationUserGame? current = entity.Favorites.FirstOrDefault(f => f.UserId == currentUserId);

            if (current != null) 
            {
                entity.Favorites.Remove(current);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Favorites));
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddComment(int gameId)
        {
            var model = new AddGameCommentViewModel()
            {
                GameId = gameId
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(AddGameCommentViewModel model, int gameId)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            GameComment comment = new GameComment()
            {
                Text = model.Text,
                UserId = GetCurrentUserId(),
                GameId = gameId,
            };

            await context.GameComments.AddAsync(comment);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Game), new {id = gameId});
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await context.GameComments.FindAsync(commentId);

            if (comment == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!(comment?.UserId == GetCurrentUserId() || User.IsInRole("Admin")))
            {
                return RedirectToAction(nameof(Game), new {id = comment.GameId});
            }

            context.GameComments.Remove(comment);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Game), new { id = comment.GameId });
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
