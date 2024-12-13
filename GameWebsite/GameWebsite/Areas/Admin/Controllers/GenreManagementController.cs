using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.ViewModels;
using GameWebsite.Web.ViewModels.AdminViewModels;
using GameWebsite.Web.ViewModels.Artwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameWebsite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GenreManagementController : Controller
    {
        private readonly IGenreService genreService;

        public GenreManagementController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genreViewModels = await genreService.GetAllAsync();

            return View(genreViewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGenreViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await this.genreService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await genreService.GetByIdForEditAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddGenreViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            Genre? entity = await genreService.GetByIdAsync(id);

            if (entity == null) 
            {
                return RedirectToAction(nameof(Index));
            }

            await genreService.UpdateAsync(entity, model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => DeleteGenre(id));

            return RedirectToAction("Index");
        }

        [HttpPost]
        private async Task DeleteGenre(int id)
        {
            Genre? entity = await genreService.GetByIdAsync(id);

            if (entity != null)
            {
                await genreService.DeleteAsync(id);
            }
        }
    }
}
