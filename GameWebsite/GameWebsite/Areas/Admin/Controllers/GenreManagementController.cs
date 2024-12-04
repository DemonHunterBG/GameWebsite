using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Web.Areas.Admin.ViewModels;
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
        private readonly ApplicationDbContext context;

        public GenreManagementController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genreViewModels = await context.Genres
                .Select(g => new GenreViewModel()
                {
                    Id = g.Id,
                    GenreName = g.GenreName,
                })
                .AsNoTracking()
                .ToListAsync();


            return View(genreViewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGenreInputModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            Genre Genre = new Genre()
            {
                GenreName = model.GenreName
            };

            await context.Genres.AddAsync(Genre);
            await context.SaveChangesAsync();

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
            Genre? entity = await context.Genres.FindAsync(id);

            if (entity != null)
            {
                context.Genres.Remove(entity);

                await context.SaveChangesAsync();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Genres
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(g => new AddGenreInputModel
                {
                    GenreName = g.GenreName,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddGenreInputModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            Genre? entity = await context.Genres.FindAsync(id);

            if (entity == null) 
            {
                return RedirectToAction(nameof(Index));
            }

            entity.GenreName = model.GenreName;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
