using System.Xml.Serialization;
using CarDealer.Models;

namespace CarDealer.DTOs.Export
{
    [XmlType("car")]
    public class CarWithPartsDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("traveled-distance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public PartsForCarsDto[] Parts { get; set; }
    }

    
}
