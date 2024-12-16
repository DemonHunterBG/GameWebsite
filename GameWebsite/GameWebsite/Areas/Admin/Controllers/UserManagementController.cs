using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameWebsite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userViewModels = await userService.GetAllAsync();

            return View(userViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            await userService.AssignRoleAsync(userId, role);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            await userService.RemoveRoleAsync(userId, role);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await userService.DeleteUserAsync(userId);

            return RedirectToAction("Index");
        }
    }
}
