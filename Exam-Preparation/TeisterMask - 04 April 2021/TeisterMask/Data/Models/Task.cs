﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.Data.Models
{
    public class Task
    {
        public Task()
        {
            EmployeesTasks = new HashSet<EmployeeTask>();
        }

        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(40)] 
        public string Name { get; set; } = null!;

        [Required]
        public DateTime OpenDate { get; set; }
       
        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public ExecutionType ExecutionType { get; set; }

        [Required]
        public LabelType LabelType { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;

        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
