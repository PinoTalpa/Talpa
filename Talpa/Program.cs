using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Talpa.Support;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Services;
using Talpa_DAL.Data;
using Talpa_DAL.Interfaces;
using Talpa_DAL.Repositories;

namespace Talpa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(10, 4, 28)), // Edit this to your SQL server version.
                    mySqlOptions =>
                    {
                        mySqlOptions.MigrationsAssembly("Talpa");
                        mySqlOptions.EnableStringComparisonTranslations(); // Enable string comparison translations
                    });
            });

            builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<ISuggestionService, SuggestionService>();
            builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();

            builder.Services.AddScoped<IActivityService, ActivityService>();
            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();

            builder.Services.AddScoped<IActivityDateService, ActivityDateService>();
            builder.Services.AddScoped<IActivityDateRepository, ActivityDateRepository>();

            builder.Services.AddScoped<IQuarterService, QuarterService>();
            builder.Services.AddScoped<ILimitationService, LimitationService>();
            builder.Services.AddScoped<ILimitationRepository, LimitationRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuth0WebAppAuthentication(options =>
            {
                options.Domain = builder.Configuration["Auth0:Domain"];
                options.ClientId = builder.Configuration["Auth0:ClientId"];
                options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
            });

            // Configure the HTTP request pipeline.
            builder.Services.ConfigureSameSiteNoneCookies();

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddRazorPages().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            var app = builder.Build();

            var supportedCultures = new[] { "en-US", "nl-NL" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "MyAreaAdmin",
                areaName: "Admin",
                pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}