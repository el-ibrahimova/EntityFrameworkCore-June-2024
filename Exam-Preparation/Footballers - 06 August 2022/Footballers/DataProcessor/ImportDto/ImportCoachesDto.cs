using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Coach")]
    public class ImportCoachesDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("Nationality")]
      public string Nationality { get; set; } = null!;
     
        [XmlArray("Footballers")] 
        public ImportFootballersDto[] Footballers { get; set; } = null!;
    }
}
