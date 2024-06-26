﻿using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }

        // DateTime? - this column is Nullable -  not required
        public DateTime? Birthday { get; set; }



        //relation to the StudentCourse table: one student to many courses
        public virtual ICollection<StudentCourse> StudentsCourses { get; set; }

        //relation to Homework table: one student to many homeworks
        public virtual ICollection<Homework> Homeworks { get; set; }
    }
}
