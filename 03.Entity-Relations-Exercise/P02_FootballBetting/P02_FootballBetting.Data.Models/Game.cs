using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        // in real project it is good the PK to be string -> GUID
        [Key]
        public int GameId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }
        public byte HomeTeamGoals { get; set; }
        public byte AwayTeamGoals { get;set; }

        // DateTime is required by default!
        // DateTime? is nullable
        public DateTime DateTime { get; set; }

        public double HomeTeamBetRate { get; set; }
        public double AwayTeamBetRate { get; set; }

        public double DrawBetRate { get; set; }

        [MaxLength(ValidationConstants.GameResultMaxLength)]
        public string? Result { get; set; }
        // in is nullable - string? - bacause of the game may be not finished 
    }
}
