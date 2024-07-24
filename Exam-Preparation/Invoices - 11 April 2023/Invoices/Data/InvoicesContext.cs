using Invoices.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Invoices.Data
{
    public class InvoicesContext : DbContext
    {
        public InvoicesContext() 
        { 
        }

        public InvoicesContext(DbContextOptions options)
            : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()  // activate Lazy Loading
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;

        public DbSet<Invoice> Invoices { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<ProductClient> ProductsClients { get; set; } = null!;

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductClient>()
                .HasKey(p => new { p.ClientId, p.ProductId });
        }
    }
}
