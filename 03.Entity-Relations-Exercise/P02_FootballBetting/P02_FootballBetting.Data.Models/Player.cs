using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models
{
    public class Player
    {
        public Player()
        {
            this.PlayersStatistic = new HashSet<PlayerStatistic>();
        }

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
        [ForeignKey(nameof(Team))]
        public int?  TeamId { get; set; } 
        public virtual Team? Team { get; set; }



        // Position relations
        [ForeignKey(nameof(Position))]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }




        // from mapping table PlayerStatistic 
        public virtual ICollection<PlayerStatistic> PlayersStatistic { get; set; }
    }
}
