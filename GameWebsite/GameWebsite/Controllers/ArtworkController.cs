using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Services.Data;
using GameWebsite.Services.Data.Interfaces;
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
        private readonly IArtworkService artworkService;

        public ArtworkController(ApplicationDbContext context, IArtworkService artworkService)
        {
            this.context = context;
            this.artworkService = artworkService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 12)
        {
            var artworks = await artworkService.GetAllAsync();

            int totalArtworks = artworks.Count();
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
            Artwork? entity = await artworkService.GetByIdAsync(id);

            if (entity != null)
            {
                await artworkService.DeleteAsync(id);
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

            await artworkService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
