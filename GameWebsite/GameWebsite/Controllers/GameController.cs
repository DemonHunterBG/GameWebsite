using GameWebsite.Data;
using GameWebsite.Web.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameWebsite.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext context;

        public GameController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await context.Games
                .Select(g => new GameListViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageURL = g.ImageURL,
                    HasFavored = g.Favorites.Any(f => f.UserId == GetCurrentUserId()),
                })
                .AsNoTracking()
                .ToListAsync();

            return View(games);
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
