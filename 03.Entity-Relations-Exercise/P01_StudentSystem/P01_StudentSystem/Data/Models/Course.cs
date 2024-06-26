using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        [Key]// Може и да не се добавя, защото името съдържа Id и EF само се досеща, че е първичен ключ
        public int CourseId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }


        //relation to the StudentCourse table: one course to many students
        public virtual ICollection<StudentCourse> StudentsCourses { get; set; }

        //relation to Resources table: one course to many resources
        public virtual ICollection<Resource> Resources { get; set; }

        //relation to Homework table: one course to many homeworks
        public virtual ICollection<Homework> Homeworks { get; set; }
    }
}
