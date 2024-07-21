using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Despatcher")]
    public class ExportDespatchersWithTrucksDto
    {
        [XmlAttribute("TrucksCount")]
        public int TrucksCount { get; set; }

        [XmlElement("DespatcherName")]
        public string Name { get; set; } = null!;

        [XmlArray("Trucks")]
        public ExportTrucksDespatcherDto[] Trucks { get; set; } = null!;
    }
}
