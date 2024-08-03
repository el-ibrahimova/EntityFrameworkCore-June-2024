using System.Xml.Serialization;

namespace TravelAgency.DataProcessor.ExportDtos
{
    [XmlType("Guide")]
    public class ExportSpanishSpeakingGuidesDto
    {
        [XmlElement("FullName")]
        public string FullName { get; set; }

        [XmlArray("TourPackages")]
        public ExportTourPackagesDto[] TourPackages { get; set; }

    }

    [XmlType("TourPackage")]
    public class ExportTourPackagesDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
