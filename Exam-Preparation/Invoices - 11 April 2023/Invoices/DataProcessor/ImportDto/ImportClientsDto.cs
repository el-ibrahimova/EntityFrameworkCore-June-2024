using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Invoices.Common.ValidationConstants;


namespace Invoices.DataProcessor.ImportDto
{
    using Data.Models;

    [XmlType(nameof(Client))]
    public class ImportClientsDto
    {
        [XmlElement(nameof(Name))] 
        [Required]
        [MinLength(ClientNameMinLength)]
        [MaxLength(ClientNameMaxLength)]
        public string Name { get; set; } = null!;


         [XmlElement(nameof(NumberVat))] 
         [Required]
        [MinLength(ClientNumberVatMinLength)]
        [MaxLength(ClientNumberVatMaxLength)]
        public string NumberVat { get; set; } = null!;


        [XmlArray(nameof(Addresses))] 
        public ImportAddressesDto[] Addresses { get; set; } = null!;
    }
}
