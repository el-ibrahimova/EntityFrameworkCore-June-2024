using System.ComponentModel;
using EventMiWorkshopMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventMiWorkshopMVC.Data
{
    public class EventMiDbContext:DbContext
    {
        public EventMiDbContext()
        {
            
        }

        // this constructor is very important to exist
        public EventMiDbContext(DbContextOptions options)
        :base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; } = null!;

        // in web it is not good idea to use LazyLoading
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
