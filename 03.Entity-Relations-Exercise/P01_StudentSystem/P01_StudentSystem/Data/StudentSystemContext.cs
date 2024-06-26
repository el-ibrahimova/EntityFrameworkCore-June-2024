using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext:DbContext
    {
        private const string ConnectionString =
            "Server=DESKTOP-SENJ7PO\\SQLEXPRESS;Database=StudentSystem;Integrated Security=True";

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<Resource> Resoses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // за това трябва да имаме инсталиран провайдър Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // описваме композитния ключ
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc=> new {sc.CourseId, sc.StudentId})
                
                
                ;

        }



    }
}
