using Cadastre.Data.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace Cadastre.Data
{
    using Microsoft.EntityFrameworkCore;
    public class CadastreContext : DbContext
    {
        public CadastreContext()
        {
            
        }

        public CadastreContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        public DbSet<Citizen> Citizens { get; set; } = null!;
        public DbSet<District> Districts { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<PropertyCitizen> PropertiesCitizens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .Property(p => p.PostalCode)
                .IsFixedLength(true)
                .HasAnnotation("RegularExpression", "^[A-Z]{2}-[0-9]{5}");

            modelBuilder.Entity<PropertyCitizen>()
                .HasKey(pc => new { pc.CitizenId, pc.PropertyId });

        }
    }
}
