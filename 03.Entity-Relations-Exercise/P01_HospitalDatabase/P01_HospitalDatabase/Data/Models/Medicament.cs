﻿using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class Medicament
    {
        [Key]
        public string MedicamentId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
