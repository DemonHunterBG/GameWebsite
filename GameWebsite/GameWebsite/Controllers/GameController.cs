﻿using GameWebsite.Data;
using GameWebsite.Data.Models;
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

        [HttpGet]
        public async Task<IActionResult> Game(int id)
        {
            var model = await context.Games
                .Where(g => g.Id == id)
                .AsNoTracking()
                .Select(g => new GameViewModel()
                {
                    Name = g.Name,
                    GameURL = g.GameURL,
                    IsGameURLWorking = false,
                    Description = g.Description,
                    AddedOn = g.AddedOn,
                    Genres = g.Genres.Select(g => g.Genre.GenreName).ToList(),
                    Comments = g.Comments.Select(c => new GameCommentViewModel()
                    {
                        Text = c.Text,
                        AddedOn = c.AddedOn,
                        CreatorName = c.User.UserName,
                        IsCreator = c.UserId == GetCurrentUserId() ? true : false,
                    }).ToList(),
                })
                .FirstOrDefaultAsync();

            Uri uriResult;
            bool result = Uri.TryCreate(model.GameURL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (uriResult != null && uriResult.IsAbsoluteUri)
            {
                HttpWebResponse response = null;
                var request = (HttpWebRequest)WebRequest.Create(model.GameURL + "/index.html");
                request.Method = "HEAD";

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    /* A WebException will be thrown if the status of the response is not `200 OK` */
                }
                finally
                {
                    if (response != null)
                    {
                        model.IsGameURLWorking = true;
                        response.Close();
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var games = await context.Games
                .Where(g => g.Favorites.Any(f => f.UserId == GetCurrentUserId()))
                .Select(g => new GameListViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageURL = g.ImageURL,
                    HasFavored = true,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(games);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int gameId)
        {
            Game? entity = await context.Games
                .Where(g => g.Id == gameId)
                .Include(g => g.Favorites)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                return RedirectToAction(nameof(Index));
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!entity.Favorites.Any(f => f.UserId == currentUserId))
            {
                entity.Favorites.Add(new ApplicationUserGame()
                {
                    UserId = currentUserId,
                    GameId = gameId
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Favorites));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int gameId)
        {
            Game? entity = await context.Games
            .Where(g => g.Id == gameId)
            .Include(g => g.Favorites)
            .FirstOrDefaultAsync();

            if (entity == null)
            {
                return RedirectToAction(nameof(Favorites));
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            ApplicationUserGame? current = entity.Favorites.FirstOrDefault(f => f.UserId == currentUserId);

            if (current != null) 
            {
                entity.Favorites.Remove(current);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Favorites));
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
