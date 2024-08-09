using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Infrastructure.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }


        // we set it to be unique with fluent API in dbContext
        [Required]
        [MaxLength(64)]
        public string Username { get; set; }

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
