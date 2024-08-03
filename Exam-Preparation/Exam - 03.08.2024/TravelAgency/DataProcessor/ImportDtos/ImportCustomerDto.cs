using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ImportDtos
{
    [XmlType(nameof(Customer))]
    public class ImportCustomerDto
    {
        [XmlAttribute("phoneNumber")]
        [Required]
        [RegularExpression(@"^\+\d{12}$")]
        public string PhoneNumber { get; set; }

        [XmlElement("FullName")]
        [Required]
        [MinLength(4)]
        [MaxLength(60)]
        public string FullName { get; set; }

        [XmlElement("Email")]
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
