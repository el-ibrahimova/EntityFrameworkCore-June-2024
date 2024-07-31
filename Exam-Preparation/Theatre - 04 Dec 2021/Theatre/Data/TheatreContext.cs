
using Theatre.Data.Models;

namespace Theatre.Data
{
    using Microsoft.EntityFrameworkCore;

    public class TheatreContext : DbContext
    {
        public TheatreContext() 
        {
        }

        public TheatreContext(DbContextOptions options)
        : base(options) 
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                 .UseSqlServer(Configuration.ConnectionString);
            }
        }

        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Cast> Casts { get; set; } = null!;
        public DbSet<Play> Plays { get; set; } = null!;
        public DbSet<Models.Theatre> Theatres { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasKey(t => new { t.TheatreId, t.PlayId });
        }
    }
}