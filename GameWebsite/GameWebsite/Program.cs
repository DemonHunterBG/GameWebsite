using GameWebsite.Data;
using GameWebsite.Data.Configuration;
using GameWebsite.Data.Models;
using GameWebsite.Data.Repository;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Services.Data;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameWebsite.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => ConfigureIdentity(options, builder)) //CHANGED TO FALSE
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(IGameService).Assembly);

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                DatabaseSeeder.SeedRoles(serviceProvider);
                DatabaseSeeder.AssignAdminRole(serviceProvider);
            }

            app.Run();
        }

        private static void ConfigureIdentity(IdentityOptions cfg, WebApplicationBuilder builder)
        {
            cfg.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
            cfg.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUpperCase");
            cfg.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowerCase");
            cfg.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
            cfg.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequireLength");
            cfg.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Identity:Password:RequireUniqueCharacters");

            cfg.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            cfg.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            cfg.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            cfg.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }
    }
}