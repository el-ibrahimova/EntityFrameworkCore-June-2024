using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext:DbContext
    {
        private const string ConnectionString =
            "Server=DESKTOP-SENJ7PO\\SQLEXPRESS;Database=HospitalDatabase;Integrated Security=True";   
        
        public HospitalContext()
        {
            
        }

        public HospitalContext(DbContextOptions options)
        :base(options)
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .Property(p => p.Email)
                .IsUnicode(false);
            
        }
    }
}
