using GameWebsite.Data;
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
    }
}
