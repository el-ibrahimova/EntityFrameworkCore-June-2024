using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType(nameof(Manufacturer))]
    public class ImportManufacturerDto
    {
        [Required]
        [XmlElement(nameof(ManufacturerName))]
        [MinLength(ValidationConstants.ManufacturerNameMinLength)]
        [MaxLength(ValidationConstants.ManufacturerNameMaxLength)]
        public string ManufacturerName { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Founded))]
        [MinLength(ValidationConstants.ManufacturerFoundedMinLength)]
        [MaxLength(ValidationConstants.ManufacturerFoundedMaxLength)]
        public string Founded { get; set; } = null!;

    }
}
