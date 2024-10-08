﻿using System.ComponentModel.DataAnnotations;
using Trucks.Common;

namespace Trucks.Data.Models
{
    public class Despatcher
    {
        public Despatcher()
        {
            Trucks = new HashSet<Truck>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DespatcherNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ValidationConstants.DespatcherPositionMaxLength)]
        public string Position { get; set; }

        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
