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
        [XmlElement(nameof(Rating))]
        [Range(0, 10)]
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
