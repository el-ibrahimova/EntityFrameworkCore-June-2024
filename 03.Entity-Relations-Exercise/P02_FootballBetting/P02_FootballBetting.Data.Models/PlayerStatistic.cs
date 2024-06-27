namespace P02_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        // Here we have composite PK -> we will use Fluent API for config it
        public int GameId { get; set; }
        public int PlayerId { get; set; }

        // Warning: Judge may not be happy with byte
        public byte ScoredGoals { get; set; }

        public byte Assists { get; set; }
        public byte MinutesPlayed { get; set;}
    }
}
