using Trucks.Data.Models;

namespace Trucks.Data
{
    using Microsoft.EntityFrameworkCore;

    public class TrucksContext : DbContext
    {
        public TrucksContext()
        { 
        }

        public TrucksContext(DbContextOptions options)
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

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<ClientTruck> ClientsTrucks { get; set; } = null!;
        public DbSet<Despatcher> Despatchers { get; set; } = null!;
        public DbSet<Truck> Trucks { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientTruck>(e =>
                e.HasKey(c => new {c.ClientId, c.TruckId}));
        }
    }
}
