using System.ComponentModel.DataAnnotations.Schema;

namespace P01_StudentSystem.Data.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        
        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; } // трябва да е virtual заради LazyLoading. По принцип е добре навигационните пропъртита да са virtual
       
        public int CourseId { get; set; }
       
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }
    }
}
