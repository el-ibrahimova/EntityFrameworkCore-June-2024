using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("Users")]
    public class ExportUsersProductsDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")] 
        public ExportUsersWithProducts[] UsersWithProducts { get; set; } = null!;
    }
}
