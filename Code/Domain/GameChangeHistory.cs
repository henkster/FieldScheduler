namespace Domain
{
    public class GameChangeHistory : Game
    {
        public GameStatus Status { get; set; }
        public int GameId { get; set; }
    }

    public enum GameStatus
    {
        Scheduled,
        Modified,
        Canceled
    }
}