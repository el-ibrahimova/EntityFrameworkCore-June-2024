using CinemaApp.Core.Contracts;
using CinemaApp.Infrastructure.Data;
using CinemaApp.Infrastructure.Data.Common;
using CinemaApp.Core.Models;
using CinemaApp.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true)
    .AddUserSecrets(typeof(Program).Assembly)
    .Build();

// inversion of control container == serviceProvider
var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddDbContext<CinemaDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("CinemaConnection")))
    .AddScoped<IRepository, Repository>() // ако в някакъв момент искаме да сменим Entity Framework, можем да променим само <IRepository, Repository> 
    .AddScoped<ICinemaService, CinemaService>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
ICinemaService? service = scope.ServiceProvider.GetService<ICinemaService>();

//if (service != null)
//{
//    var cinema = new CinemaModel("Capitol", "Sofia, Car Boris III");

//    await service.AddCinemaAsync(cinema);
//}