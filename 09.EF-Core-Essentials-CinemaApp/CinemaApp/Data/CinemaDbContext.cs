using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CinemaApp.Data
{   
    public class CinemaDbContext : DbContext
    {
        // за са създадем базата (да изпълним миграцията) този конструктор трябва да бъде закоментиран. откоментираме го, когато вече имаме създадена база и закоментираме другия конструктор и метода OnConfiguring
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options)
        {
        }

        // за да с// за са създадем базата (да изпълним миграцията) този конструктор не трябва да бъде закоментиран. закоментираме го след като вече имаме създадена база

        //public CinemaDbContext()
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    IConfiguration configuration = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json", true)
        //        .AddUserSecrets(typeof(Program).Assembly)
        //        .Build();

        //    if (optionsBuilder.IsConfigured == false)
        //    {
        //        optionsBuilder.UseSqlServer(configuration.GetConnectionString("CinemaConnection"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany(s => s.Tickets)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Tariff)
                .WithMany(t => t.Tickets)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }
    }
}
