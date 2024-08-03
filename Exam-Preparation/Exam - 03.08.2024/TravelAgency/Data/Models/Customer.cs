using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
