using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Medicine")]
    public class ExportMedicinesBestBeforeDto
    {
        [XmlAttribute("Category")] 
        public string Category { get; set; } = null;
        public string Name { get; set; } = null!;

        public string Price { get; set; } = null!;
        public string Producer { get; set; } = null!;
        [XmlElement("BestBefore")]
        public string ExpiryDate { get; set; }= null!;

    }
}
