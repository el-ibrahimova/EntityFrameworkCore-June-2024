using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Data.Models
{
    public class TourPackage
    {
        public TourPackage()
        {
            Bookings = new HashSet<Booking>();
            TourPackagesGuides = new HashSet<TourPackageGuide>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string PackageName { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<TourPackageGuide> TourPackagesGuides { get; set; }
    }
}
