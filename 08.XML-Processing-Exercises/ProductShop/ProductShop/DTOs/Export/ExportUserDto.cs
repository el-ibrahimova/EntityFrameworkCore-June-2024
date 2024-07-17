using System.Xml.Serialization;
using ProductShop.Models;

namespace ProductShop.DTOs.Export
{
    [XmlType("User")]
    public class ExportUserDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; } = null!;

        [XmlElement("lastName")] 
        public string LastName { get; set; } = null!;

        [XmlArray("soldProducts")] 
        public ExportProductsDto[] ProductSold { get; set; } = null!;
    }


  

}
