using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Boardgames.Common;
using Boardgames.Data.Models.Enums;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Creator")]
    public class ImportCreatorDto
    {

        [Required]
        [MinLength(ValidationConstants.CreatorNameMinLength)]
        [MaxLength(ValidationConstants.CreatorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ValidationConstants.CreatorNameMinLength)]
        [MaxLength(ValidationConstants.CreatorNameMaxLength)]
        public string LastName { get; set; }

        [XmlArray("Boardgames")]
        public BoardgameDto[] Boardgames { get; set; }

    }

    [XmlType("Boardgame")]
    public class BoardgameDto
    {
        [Required]
        [MinLength(ValidationConstants.BoardgameNameMinLength)]
        [MaxLength(ValidationConstants.BoardgameNameMaxLength)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [Range(ValidationConstants.BoardgameMinRating, ValidationConstants.BoardgameMaxRating)]
        [XmlElement("Rating")]
        public double Rating { get; set; }

        [XmlElement("CategoryType")]
        public int CategoryType { get; set; }

        [Required]
        [Range(ValidationConstants.BoardgameMinYearPublished, ValidationConstants.BoardgameMaxYearPublished)]
        [XmlElement("YearPublished")]
        public int YearPublished { get; set; }

        [Required]
        [XmlElement("Mechanics")]
        public string Mechanics { get; set; }
    }
}
