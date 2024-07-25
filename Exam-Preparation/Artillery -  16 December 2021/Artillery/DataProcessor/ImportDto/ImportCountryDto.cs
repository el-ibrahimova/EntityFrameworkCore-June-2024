using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType(nameof(Country))]
    public class ImportCountryDto
    {
        [Required]
        [XmlElement(nameof(CountryName))]
        [MaxLength(ValidationConstants.CountryNameMaxLength)]
        [MinLength(ValidationConstants.CountryNameMinLength)]
        public string CountryName { get; set; } = null!;
        

        [Required]
        [XmlElement(nameof(ArmySize))]
        [Range(ValidationConstants.CountryArmySizeMinValue, ValidationConstants.CountryArmySizeMaxValue)]
        public int ArmySize { get; set; }
    }
}
