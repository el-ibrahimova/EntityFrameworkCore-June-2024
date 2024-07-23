using System.Xml.Serialization;

namespace Footballers.DataProcessor.ExportDto
{
    [XmlType("Coach")]
    public class ExportCoachWithFootballersDto
    {
        [XmlAttribute]
        public int FootballersCount { get; set; }

        public string CoachName { get; set; }
        
        [XmlArray]
        public ExportFootballersDto[] Footballers { get; set; }
    }
}
