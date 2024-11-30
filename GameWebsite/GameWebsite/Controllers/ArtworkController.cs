using GameWebsite.Data;
using GameWebsite.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            var artworks = await context.Artworks
                .AsNoTracking()
                .ToListAsync();

            return View(artworks);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => DeleteArtwork(id));

            return RedirectToAction("Index");
        }

        private async Task DeleteArtwork(int id)
        {
            Artwork? entity = await context.Artworks.FindAsync(id);

            if (entity != null)
            {
                context.Artworks.Remove(entity);

                await context.SaveChangesAsync();
            }
        }
    }
}
