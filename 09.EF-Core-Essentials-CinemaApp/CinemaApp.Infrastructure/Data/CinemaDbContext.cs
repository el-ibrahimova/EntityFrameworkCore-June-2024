﻿using System.Reflection;
using CinemaApp.Infrastructure.Data.Extension;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CinemaApp.Infrastructure.Data
{
    public class CinemaDbContext : DbContext
    {
        // за са създадем базата (да изпълним миграцията) този конструктор трябва да бъде закоментиран. откоментираме го, когато вече имаме създадена база и закоментираме метода OnConfiguring
        //public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        //    : base(options)
        //{
        //}

        //public CinemaDbContext()
        //{

        //}


        //  за да създадем базата(да изпълним миграцията) този метод не трябва да бъде закоментиран.закоментираме го след като вече имаме създадена база

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddUserSecrets("69c38dfb-32b9-44a6-82ac-cd4f0b38e8fc")  
                // тук трябваше да сработи така ..AddUserSecrets(Assembly.GetEntryAssembly()) // assembly = system reflection , но не намери connestin string-a, затова го взехме наготово от CinemaApp -> Dependencies - > UserSecretId = "69c38dfb-32b9-44a6-82ac-cd4f0b38e8fc"
                .Build();

            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("CinemaConnection"));
            }
        }


        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<User> Users { get; set; }


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

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<CinemaHall>()
                .HasKey(ch => new { ch.CinemaId, ch.HallId });

            // seed the data from ModelBuilderExtension class in Extension folder
             modelBuilder.Seed();
           

            // it is good practice to write this method, not to delete it
            base.OnModelCreating(modelBuilder);
        }
    }
}
