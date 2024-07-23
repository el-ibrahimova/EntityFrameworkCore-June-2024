﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Footballers.DataProcessor.ImportDto
{
    public class ImportTeamsDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression( @"^[A-Za-z0-9\s\.\-]{3,}$")]
       public string Name { get; set; } = null!;

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
   public string Nationality { get; set; } = null!;
        
        [Required]
      public int Trophies { get; set; }

        [JsonProperty("Footballers")]
        public int[] Footballes { get; set; } 
    }
}
