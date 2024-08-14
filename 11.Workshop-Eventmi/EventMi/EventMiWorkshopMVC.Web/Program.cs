using System.Reflection.Emit;
using EventMiWorkshopMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace EventMiWorkshopMVC.Web
{
    public class Program
    {

        // every async methods return Task, not void
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            // this "Default" string is placed in appsettings.json -> appsettings.Development.json
            // in this way we can use several connection strings
            string connectionString = builder.Configuration.GetConnectionString("Default");

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // method AddDbContext is allowed after installing Microsoft.Extensions.DependencyInjection in EventMiWorkshopMVC.Web project
            builder.Services.AddDbContext<EventMiDbContext>(cfg =>
                cfg.UseSqlServer(connectionString));


            WebApplication? app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();  // load files from wwwroot

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // every new migration is applied on application re-run
            using IServiceScope scope = app.Services.CreateScope();
            EventMiDbContext db = scope.ServiceProvider.GetRequiredService<EventMiDbContext>();
            await db.Database.MigrateAsync();

            await app.RunAsync();
        }
    }
}