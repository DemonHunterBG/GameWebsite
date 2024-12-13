using GameWebsite.Data;
using GameWebsite.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameWebsite.Web.ViewModels.AdminViewModels;

namespace GameWebsite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GameManagementController : Controller
    {
        private readonly ApplicationDbContext context;

        public GameManagementController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allGenres = await context.Genres.ToListAsync();

            var gameViewModels = await context.Games
                .Select(g => new GameManagementViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Genres = g.Genres.Select(g => g.Genre).ToList(),
                    AllGenres = allGenres,
                })
                .AsNoTracking()
                .ToListAsync();


            return View(gameViewModels);
        }


        [HttpPost]
        public async Task<IActionResult> AssignGenre(int gameId, int genreId)
        {
            if (!await context.GamesGenres.AnyAsync(gg => gg.GameId == gameId && gg.GenreId == genreId))
            {
                GameGenre gameGenre = new GameGenre()
                {
                    GameId = gameId,
                    GenreId = genreId,
                };

                await context.GamesGenres.AddAsync(gameGenre);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveGenre(int gameId, int genreId)
        {
            if (await context.GamesGenres.AnyAsync(gg => gg.GameId == gameId && gg.GenreId == genreId))
            {
                GameGenre? gameGenre = await context.GamesGenres.FindAsync(gameId, genreId);

                if (gameGenre != null)
                {
                    context.GamesGenres.Remove(gameGenre);
                    await context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGameViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            Game game = new Game()
            {
                Name = model.Name,
                GameURL = model.GameURL,
                ImageURL = model.ImageURL,
                Description = model.Description,
            };

            await context.Games.AddAsync(game);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Games
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(g => new AddGameViewModel
                {
                    Name = g.Name,
                    GameURL = g.GameURL,
                    ImageURL = g.ImageURL,
                    Description = g.Description,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddGameViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            Game? entity = await context.Games.FindAsync(id);

            if (entity == null)
            {
                return RedirectToAction(nameof(Index));
            }

            entity.Name = model.Name;
            entity.GameURL = model.GameURL;
            entity.ImageURL = model.ImageURL;
            entity.Description = model.Description;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => DeleteGame(id));

            return RedirectToAction("Index");
        }

        [HttpPost]
        private async Task DeleteGame(int id)
        {
            Game? entity = await context.Games.FindAsync(id);

            if (entity != null)
            {
                List<GameGenre> gameGenres = await context.GamesGenres
                    .Where(gg => gg.GameId == id)
                    .ToListAsync();

                List<ApplicationUserGame> applicationUserGames = await context.ApplicationUsersGames
                    .Where(gg => gg.GameId == id)
                    .ToListAsync();

                List<GameComment> gameComments = await context.GameComments
                    .Where(gg => gg.GameId == id)
                    .ToListAsync();

                context.GamesGenres.RemoveRange(gameGenres);
                context.ApplicationUsersGames.RemoveRange(applicationUserGames);
                context.GameComments.RemoveRange(gameComments);
                context.Games.Remove(entity);

                await context.SaveChangesAsync();
            }
        }
    }
}
