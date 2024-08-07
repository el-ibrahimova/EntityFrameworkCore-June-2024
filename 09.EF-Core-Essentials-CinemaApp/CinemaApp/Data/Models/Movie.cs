using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Data.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
