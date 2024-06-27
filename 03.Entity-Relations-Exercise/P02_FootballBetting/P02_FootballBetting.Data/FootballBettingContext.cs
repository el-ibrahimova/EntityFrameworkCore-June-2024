using Microsoft.EntityFrameworkCore;

namespace P02_FootballBetting.Data
{
    public class FootballBettingContext:DbContext
    {
        // Use it when developing the application
        // when we test the application locally on our PC
        public FootballBettingContext()
        {
            
        }
        
        // used by Judge
        // loading of the DbContext with Dependency Injection -> in real application it is useful
        public FootballBettingContext(DbContextOptions options)
        : base(options) 
        {
            
        }

        // Connection configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Set default connection string
                optionsBuilder.UseSqlServer(v)
            }

            base.OnConfiguring(optionsBuilder);
        }

        // Fluent API and Entities configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}
