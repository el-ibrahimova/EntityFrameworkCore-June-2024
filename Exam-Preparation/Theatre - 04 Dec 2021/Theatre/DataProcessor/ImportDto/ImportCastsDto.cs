using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Theatre.Data.Models;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType(nameof(Cast))]
    public class ImportCastsDto
    {
        [XmlElement(nameof(FullName))]
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string FullName { get; set; } = null!;

        [Required]
        [XmlElement(nameof(IsMainCharacter))]
        public bool IsMainCharacter { get; set; } 

        [Required]
        [XmlElement(nameof(PhoneNumber))]
        [RegularExpression(@"^\+44-\d{2}-\d{3}-\d{4}$")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [XmlElement(nameof(PlayId))]
        public int PlayId { get; set; }
    }
}
