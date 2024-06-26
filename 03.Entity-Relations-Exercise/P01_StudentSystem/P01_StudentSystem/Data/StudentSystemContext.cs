using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext:DbContext
    {
        private const string ConnectionString =
            "Server=DESKTOP-SENJ7PO\\SQLEXPRESS;Database=StudentSystem;Integrated Security=True";

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // за това трябва да имаме инсталиран провайдър Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder.UseSqlServer(ConnectionString);
        }



    }
}
