using System.ComponentModel.DataAnnotations;

namespace EventMiWorkshopMVC.Data.Models
{
    using static Common.EntityConstraints;

    public class Event
    {
        [Key] public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(EventPlaceMaxLength)]
        public string Place { get; set; } = null!;

        [Required]
        public bool? IsActive { get; set; }
    }
}
