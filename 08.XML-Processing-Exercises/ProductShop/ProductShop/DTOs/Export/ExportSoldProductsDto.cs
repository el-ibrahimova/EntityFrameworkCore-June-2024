using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
  
    public class ExportSoldProductsDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")] 
        public ExportProductsDto[] Products { get; set; } = null!;
    }
}
