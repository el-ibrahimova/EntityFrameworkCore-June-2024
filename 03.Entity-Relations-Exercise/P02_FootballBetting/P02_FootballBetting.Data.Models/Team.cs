using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]  // NOT NULL constraint in SQL
        [MaxLength(ValidationConstants.TeamNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.TeamLogoUrlMaxLength)]
        public string LogoUrl { get; set; }


        [Required]
        [MaxLength(ValidationConstants.TeamInitialsMaxLength)]
        public string Initials { get; set; }

        // Required (NOT NULL) by default because decimal data type is not nullable
        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }

        public int SecondaryKitColorId { get; set; }

        public int TownId { get; set; }
    }
}
