using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Web.ViewModels.Artwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Tasks;

namespace GameWebsite.Web.Controllers
{
    public class ArtworkController : Controller
    {
        private readonly ApplicationDbContext context;

        public ArtworkController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 12)
        {
            var artworks = await context.Artworks
                .AsNoTracking()
                .ToListAsync();

            int totalArtworks = artworks.Count;
            int totalPages = (int)Math.Ceiling(totalArtworks / (double)pageSize);

            artworks = artworks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["TotalArtworks"] = totalArtworks;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(artworks);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => DeleteArtwork(id));

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        private async Task DeleteArtwork(int id)
        {
            Artwork? entity = await context.Artworks.FindAsync(id);

            if (entity != null)
            {
                context.Artworks.Remove(entity);

                await context.SaveChangesAsync();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddArtworkInputModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            Artwork artwork = new Artwork()
            {
                Title = model.Title,
                ArtworkURL = model.ArtworkURL,
            };

            await context.Artworks.AddAsync(artwork);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
