using GameWebsite.Data.Models;
using GameWebsite.Data;
using GameWebsite.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Web.ViewModels.AdminViewModels;
using Microsoft.EntityFrameworkCore;

namespace GameWebsite.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUserGame, object> applicationUserGameRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(
            IRepository<ApplicationUserGame, object> applicationUserGameRepository, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            this.applicationUserGameRepository = applicationUserGameRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                int commments = user.GameComments.Count;
                var roles = await userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList(),
                });
            }

            return(userViewModels);
        }

        public async Task AssignRoleAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null && await roleManager.RoleExistsAsync(role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task RemoveRoleAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null && await roleManager.RoleExistsAsync(role))
            {
                await userManager.RemoveFromRoleAsync(user, role);
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                List<ApplicationUserGame> applicationUserGames = await applicationUserGameRepository
                    .GetAllAttached()
                    .Where(aug => aug.UserId == userId)
                    .ToListAsync();

                foreach (var applicationUserGame in applicationUserGames)
                {
                    await applicationUserGameRepository.DeleteEntityAsync(applicationUserGame);
                }

                await userManager.DeleteAsync(user);
            }
        }
    }
}
