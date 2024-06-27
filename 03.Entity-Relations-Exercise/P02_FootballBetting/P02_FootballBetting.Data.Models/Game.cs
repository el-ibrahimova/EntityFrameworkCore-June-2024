using System.ComponentModel.DataAnnotations;

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

        public byte HomeTeamBetRate { get; set; }
        public byte AwayTeamBetRate { get; set; }
    }
}
