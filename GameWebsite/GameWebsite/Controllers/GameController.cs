using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Services.Data.Interfaces;
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
        private readonly IGameService gameService;
        private readonly IGameCommentService gameCommentService;

        public GameController(ApplicationDbContext context, IGameService gameService, IGameCommentService gameCommentService)
        {
            this.context = context;
            this.gameService = gameService;
            this.gameCommentService = gameCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchQuery = null, string? genre = null)
        {
            var games = await gameService.GetAllWithQueryAsync(GetCurrentUserId(), searchQuery, genre);

            ViewData["SearchQuery"] = searchQuery;
            ViewData["Genre"] = genre;

            return View(games);
        }

        [HttpGet]
        public async Task<IActionResult> Game(int id)
        {
            if (!await gameService.CheckIfGameExists(id))
            {
                return RedirectToAction(nameof(Index));
            }

            var model = await gameService.GetGameForPageByIdAsync(id, GetCurrentUserId());

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var games = await gameService.GetAllFavoritesAsync(GetCurrentUserId());

            return View(games);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int gameId)
        {
            Game? entity = await gameService.GetByIdFavoritesAsync(gameId);

            if (entity == null)
            {
                return RedirectToAction(nameof(Index));
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            await gameService.AddToFavoritesAsync(currentUserId, gameId);

            return RedirectToAction(nameof(Favorites));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int gameId)
        {
            Game? entity = await gameService.GetByIdFavoritesAsync(gameId);

            if (entity == null)
            {
                return RedirectToAction(nameof(Favorites));
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            await gameService.RemoveFromFavoritesAsync(currentUserId, gameId);

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

            await gameCommentService.AddCommentAsync(model, gameId, GetCurrentUserId());

            return RedirectToAction(nameof(Game), new {id = gameId});
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await gameCommentService.GetCommentByIdAsync(commentId);

            if (comment == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!(comment?.UserId == GetCurrentUserId() || User.IsInRole("Admin")))
            {
                return RedirectToAction(nameof(Game), new {id = comment.GameId});
            }

            await gameCommentService.DeleteCommentAsync(commentId);

            return RedirectToAction(nameof(Game), new { id = comment.GameId });
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
