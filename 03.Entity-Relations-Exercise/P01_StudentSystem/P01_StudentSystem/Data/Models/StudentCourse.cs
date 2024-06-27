using System.ComponentModel.DataAnnotations.Schema;

namespace P01_StudentSystem.Data.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        
        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; } // this should be virtual because of LazyLoading. It is good navigation property to be virtual
       
        public int CourseId { get; set; }
       
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }
    }
}
