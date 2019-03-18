namespace BoardgameData.Models
{
    public class PlayerPlayed
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public Played Played { get; set; }
        public int Score { get; set; }
        public bool isWinner { get; set; }
    }
}
