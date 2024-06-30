using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext:DbContext
    {
        private const string ConnectionString =
            "Server=DESKTOP-SENJ7PO\\SQLEXPRESS;Database=SalesDatabase;Integrated Security=True";

        public SalesContext()
        {
            
        }

        public SalesContext(DbContextOptions options)
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

        public DbSet<Product> Products { get; set; }
         public DbSet<Customer> Customers { get; set; }
         public DbSet<Store> Stores { get; set; }
         public DbSet<Sale> Sales { get; set; }
    }
}
