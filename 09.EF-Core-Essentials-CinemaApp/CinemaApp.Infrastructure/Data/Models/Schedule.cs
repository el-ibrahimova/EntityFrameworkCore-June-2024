﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApp.Infrastructure.Data.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

      
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

     
        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; } = null!; 
        
        [Required]
        public int MovieId { get; set; }

   
        [ForeignKey(nameof(HallId))]
        public Hall Hall { get; set; } = null!;
        
        [Required]
        public int HallId { get; set; }

        [Required]
        public int CinemaId { get; set; }
        
        [ForeignKey(nameof(CinemaId))]
        public Cinema Cinema { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
