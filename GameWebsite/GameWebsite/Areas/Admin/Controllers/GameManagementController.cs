using GameWebsite.Data;
using GameWebsite.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameWebsite.Web.ViewModels.AdminViewModels;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Services.Data;

namespace GameWebsite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GameManagementController : Controller
    {
        private readonly IGameService gameService;

        public GameManagementController(ApplicationDbContext context, IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var gameViewModels = await this.gameService.GetAllManagementAsync();

            return View(gameViewModels);
        }


        [HttpPost]
        public async Task<IActionResult> AssignGenre(int gameId, int genreId)
        {
            await gameService.AssignGenre(gameId, genreId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveGenre(int gameId, int genreId)
        {
            await gameService.RemoveGenre(gameId, genreId);

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

            await gameService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await gameService.GetByIdForEditAsync(id);

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

            Game? entity = await gameService.GetByIdAsync(id);

            if (entity == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await gameService.UpdateAsync(entity, model);

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
            Game? entity = await gameService.GetByIdAsync(id);

            if (entity != null)
            {
                await gameService.DeleteAsync(id);
            }
        }
    }
}
