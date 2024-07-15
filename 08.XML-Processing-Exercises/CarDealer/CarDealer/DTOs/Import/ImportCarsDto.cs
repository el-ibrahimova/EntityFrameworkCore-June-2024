using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Car")]
    public class ImportCarsDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("traveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public ImportPartsCarsDto[] PartIds { get; set; }
    }

    [XmlType("partId")]
    public class ImportPartsCarsDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
