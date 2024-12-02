using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWebsite.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GameWebsite.Data.Configuration
{
    public static class DatabaseSeeder
    {
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Administrator", "User" };

            foreach (var role in roles)
            {
                bool roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();

                if (!roleExists)
                {
                    var result = roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {role}");
                    }
                }
            }
        }

        public static void AssignAdminRole(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string userName = "Admin";
            string adminEmail = "administrator@example.com";
            string adminPassword = "@Admin123";

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = adminEmail
                };
                var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {adminEmail}");
                }
            }

            var isInRole = userManager.IsInRoleAsync(adminUser, "Administrator").GetAwaiter().GetResult();
            if (!isInRole)
            {
                var addRoleResult = userManager.AddToRoleAsync(adminUser, "Administrator").GetAwaiter().GetResult();
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign admin role to user: {adminEmail}");
                }
            }
        }
    }
}
