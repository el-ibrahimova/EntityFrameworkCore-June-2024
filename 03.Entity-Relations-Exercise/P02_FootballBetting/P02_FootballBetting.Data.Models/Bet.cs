using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Models.Enums;

namespace P02_FootballBetting.Data.Models
{
    public class Bet
    {
        // it is good to be GUID, but for the Judge -> int
        [Key] 
        public int BetId { get; set; }

        public decimal Amount { get; set; }

        // Enumerations are stored as integer (non-nullable)
        public Prediction Prediction { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public int GameId { get; set; }
    }
}
