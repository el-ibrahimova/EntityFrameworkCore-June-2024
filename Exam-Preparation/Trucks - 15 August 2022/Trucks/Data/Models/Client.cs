using System.ComponentModel.DataAnnotations;
using static Trucks.Common.ValidationConstants;

namespace Trucks.Data.Models
{
    public class Client
    {
        public Client()
        {
            ClientsTrucks = new HashSet<ClientTruck>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ClientNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ClientNationalityMaxValue)]
        public string Nationality { get; set; } = null!;

        [Required]
        [MaxLength(ClientTypeMaxLength)]
        public string Type { get; set; } = null!;

         public ICollection<ClientTruck> ClientsTrucks {get; set;}
    }
}
