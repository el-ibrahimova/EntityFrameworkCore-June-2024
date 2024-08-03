using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ImportDtos
{
    public class ImportBookingsDto
    {
        [Required]
        [JsonProperty("BookingDate")]
        public string BookingDate { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(60)]
        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [JsonProperty("TourPackageName")]
        public string TouPackageName { get; set; }
    }
}
