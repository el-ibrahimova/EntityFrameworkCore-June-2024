﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models
{
    public class Town
    {
        public Town()
        {
            this.Teams = new HashSet<Team>();
        }

        [Key]
        public int TownId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.TownNameMaxLength)]
        public string Name { get; set; }

       
        // Country relations
        [ForeignKey(nameof(Country))]
        public int CounryId { get; set; }
        public virtual Country Country { get; set; }


        // Teams relations
        public virtual ICollection<Team> Teams { get; set; }
    }
}
