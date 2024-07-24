using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Invoices.Common.ValidationConstants;

namespace Invoices.DataProcessor.ImportDto
{
    using Data.Models;

    [XmlType(nameof(Address))]
    public class ImportAddressesDto
    {
        [XmlElement(nameof(StreetNumber))]
        [Required]
        [MinLength(AddressStreetNameMinLength)]
        [MaxLength(AddressStreetNameMaxLength)]
        public string StreetName { get; set; } = null!;

       [XmlElement(nameof(StreetNumber))] 
       [Required]
        public int StreetNumber { get; set; }

         [XmlElement(nameof(PostCode))] 
         [Required]
        public string PostCode { get; set; } = null!;

         [XmlElement(nameof(City))]   
         [Required]
        [MinLength(AddressCityMinLength)]
        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; } = null!;

        [XmlElement(nameof(Country))] 
        [Required]
        [MinLength(AddressCountryMinLength)]
        [MaxLength(AddressCountryMaxLength)]
        public string Country { get; set; } = null!;
    }
}
