using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PlayerNameMaxLength)]
        public string Name { get; set; }

        public int SquadNumber { get; set; }

        // SQL Type for bool is bit
        // By default bool is NOT NULL, by default is required
        public bool IsInjured { get; set; }

        // this FK should not be NOT NULL
        // Warning: This may cause a problem in Judge
        public int?  TeamId { get; set; } 
        public int PositionId { get; set; }
    }
}
