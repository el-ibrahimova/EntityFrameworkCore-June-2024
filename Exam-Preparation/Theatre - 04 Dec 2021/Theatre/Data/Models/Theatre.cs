﻿using System.ComponentModel.DataAnnotations;
using Castle.Core.Logging;

namespace Theatre.Data.Models
{
    public class Theatre
    {
        public Theatre()
        {
            Tickets = new HashSet<Ticket>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)] 
        public string Name { get; set; } = null!;

        [Required]
        public sbyte NumberOfHalls { get; set; }

        [Required]
        [MaxLength(30)] 
        public string Director { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}
