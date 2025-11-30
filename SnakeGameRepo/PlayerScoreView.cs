using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    public class PlayerScoreView
    {
        public int Rank { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public string GameModeName { get; set; }
        public DateTime CreatedDate { get; set; }

        public string FormattedDate => CreatedDate.ToString("dd.MM.yyyy HH:mm");
        public string FormattedScore => Score.ToString("N0");
    }
}
