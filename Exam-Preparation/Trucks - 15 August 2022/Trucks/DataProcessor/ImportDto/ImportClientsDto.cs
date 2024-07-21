using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using static Trucks.Common.ValidationConstants;

namespace Trucks.DataProcessor.ImportDto
{
    public class ImportClientsDto
    {
        [Required]
        [MinLength(ClientNameMinLength)]
        [MaxLength(ClientNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(ClientNationalityMinValue)]
        [MaxLength(ClientNationalityMaxValue)]
        public string Nationality { get; set; } = null!;

        [Required]
        [MinLength(ClientTypeMinLength)]
        [MaxLength(ClientTypeMaxLength)]
        public string Type { get; set; } = null!;

        [JsonProperty("Trucks")]
        public int[] Trucks { get; set; } 
    }
}
