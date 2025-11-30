using System;

namespace snake
{
    public class HighScore
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public string GameMode { get; set; }
        public DateTime CreatedDate { get; set; }

        public HighScore() { }

        public HighScore(string playerName, int score, string gameMode)
        {
            PlayerName = playerName;
            Score = score;
            GameMode = gameMode;
            CreatedDate = DateTime.Now;
        }
    }
}