using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Cadastre.Common;
using Cadastre.Data.Enumerations;
using static Cadastre.Common.ValidationConstants;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("District")]
    public class ImportDistrictsDto
    {

        [XmlAttribute("Region")]
        [EnumDataType(typeof(Region))]
        [Required]
        public string Region { get; set; } = null!;


        [MinLength(DistrictNameMinLength)]
        [MaxLength(DistrictNameMaxLength)]
        [XmlElement("Name")]
        [Required]
        public string Name { get; set; } = null!;

        [MinLength(DistrictPostalCodeMinLength)]
        [MaxLength(DistrictNameMaxLength)]
        [XmlElement("PostalCode")]
        [Required]
        public string PostalCode { get; set; } = null!;

        [XmlArray("Properties")]
        public ImportPropertyDto[] Properties { get; set; } = null!;
    }
}
