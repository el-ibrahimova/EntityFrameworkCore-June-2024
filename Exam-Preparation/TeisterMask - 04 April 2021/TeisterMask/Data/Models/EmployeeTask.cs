﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TeisterMask.Data.Models
{
    public class EmployeeTask
    {
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = null!;


        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; } = null!;
    }
}
