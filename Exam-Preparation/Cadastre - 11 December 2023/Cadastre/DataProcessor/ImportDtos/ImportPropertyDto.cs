using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

using static Cadastre.Common.ValidationConstants;

namespace Cadastre.DataProcessor.ImportDtos

{

    [XmlType("Property")]
    public class ImportPropertyDto
    {
        [MinLength(PropertyIdentifierMinLength)]
        [MaxLength(PropertyIdentifierMaxLength)]
        [Required]
        [XmlElement("PropertyIdentifier")]
        public string PropertyIdentifier { get; set; } = null!;

        [Range(PropertyAreaMinValue, PropertyAreaMaxValue)]
        [Required]
        [XmlElement("Area")]
        public int Area { get; set; }

        [XmlElement("Details")]
        [MinLength(PropertyDetailsMinLength)]
        [MaxLength(PropertyDetailsMaxLength)]
        public string? Details { get; set; }

        [XmlElement("Address")]
        [MinLength(PropertyAddressMinLength)]
        [MaxLength(PropertyAddressMaxLength)]
        [Required]
        public string Address { get; set; } = null!;

        [XmlElement("DateOfAcquisition")]
        [Required]
        public string DateOfAcquisition { get; set; }
    }
}