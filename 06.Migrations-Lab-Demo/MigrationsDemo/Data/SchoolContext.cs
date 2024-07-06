using Microsoft.EntityFrameworkCore;
using MigrationsDemo.Models;

namespace MigrationsDemo.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SENJ7PO\\SQLEXPRESS;Database=School;Integrated Security = True");   
        }
    }
}
