using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Theatre.Data.Models;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType(nameof(Play))]
    public class ImportPlaysDto
    {
        [Required]
        [XmlElement(nameof(Title))]
        [MinLength(4)]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Duration))]
        public string Duration { get; set; } = null!;

        [Required]
        [XmlElement("Raiting")] // there is a typo mistake in the element name - in the output must be Raiting - we have to write it this way to work!!!
        [Range(0.00, 10.00)]
        public float Rating { get; set; }

        [Required]
        [XmlElement(nameof(Genre))] 
        public string Genre { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Description))]
        [MaxLength(700)]
        public string Description { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Screenwriter))]
        [MinLength(4)]
        [MaxLength(30)]
        public string Screenwriter { get; set; } = null!;
    }
}
