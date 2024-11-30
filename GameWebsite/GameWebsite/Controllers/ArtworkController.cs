﻿using GameWebsite.Data;
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
        public async Task<IActionResult> Index()
        {
            var artworks = await context.Artworks
                .AsNoTracking()
                .ToListAsync();

            return View(artworks);
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


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
