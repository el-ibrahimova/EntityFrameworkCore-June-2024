using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsSystem.Data.Models
{
    [Table("StudentsCourses")]
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
