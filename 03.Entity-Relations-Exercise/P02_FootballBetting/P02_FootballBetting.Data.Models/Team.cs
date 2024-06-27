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
    }
}
